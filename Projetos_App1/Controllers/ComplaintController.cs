﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projetos_App1.Helper;
using Projetos_App1.Models;
using Projetos_App1.Models.Repositories;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;
using Rotativa.AspNetCore;


namespace Projetos_App1.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRelationRepository _companyRelationRepository;
        private readonly ICompanyRepository _companyRepository;
      
        private readonly IComplaintService _complaintService;
        private readonly IWhistleblowingService _whistleblowingService;
        private readonly IAttachedFileService _attacheFileService;
        private readonly IEmail _email;

        public ComplaintController(IComplaintRepository complaintRepository,
                                   ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation,
                                   ICompanyRepository companyRepository,  IComplaintService complaintService,
                                   IWhistleblowingService whistleblowingService, IAttachedFileService attachedFileService, IEmail email)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
           
            _complaintService = complaintService;
            _whistleblowingService = whistleblowingService;
            _attacheFileService = attachedFileService;
            _email = email;

        }

        [HttpGet] //criando denuncia
        public IActionResult CreateComplaint()
        {
            var listcompanies = _companyRepository.companies.ToList(); // buscando lista de empresa existentes
           
            var listrelationship = _companyRelationRepository.companyRelations.ToList(); //lista de relações

            var complainVm = new ComplaintViewModel() //criando um obj View Model
            {
                listRelation = listrelationship,
                listCompany = listcompanies
            }
            ;

            return View(complainVm);
        }

        public JsonResult GetCategoryByID(int companyId)// para dropdown 
        {
            var categories = _categoryRepository.GetCategoryByID(companyId) //basiado no no id da empresa escolhida,
                .Select(c => new                                            // busco as categorias existentes
                {
                    categoryId = c.CategoryId,
                    categories = c.Categories
                }).ToList();
            return Json(categories);
        }



        [HttpPost]
        //criar uma denúncia
        public IActionResult CreateComplaint(ComplaintViewModel complaintVm)
        {
        

            // Verifica se o modelo é válido
            if (!ModelState.IsValid)
            {

                return RedirectToAction("InfoTypeComplaint");
            }

           

            Complaint complaint = _complaintService.SaveComplaint(complaintVm);//Retorna para pode add em outras tabelas

          

            if (complaintVm.Name != null)//verifca se é confidencial
            {
                _whistleblowingService.SaveWhistleblowing(complaintVm, complaint.ComplaintId);

            }
      
            if (complaintVm._files != null && complaintVm._files.Count > 0)//verifica se existe arquivo para salvar
            {

                List<AttachedFile> attachedFiles = _attacheFileService.UploadImg(complaintVm._files);
                _attacheFileService.SaveAttachedFile(attachedFiles, complaint.ComplaintId);
            }



            return View("Views/Complaint/ShowLogin.cshtml", complaint); //retorna para mostrar os dados de acesso
        }

        public IActionResult ShowLogin()
        {

            return View();
        }


        [HttpPost]
        public IActionResult ShowLogin(Complaint complaint, string email)
        {

            //verifica se esta vazio, se sim, ja redireciona para view
            if (string.IsNullOrWhiteSpace(email))
            {

                return RedirectToAction("Index", "Home");
            }

            // se não, envia email.

            // corpo do e-mail 
            var message = $"Este é o seu código de acesso: {complaint.ComplaintId} e este é a sua senha: {complaint.PassWord}";
                        


            //chamando a função que criei no helper
            _email.SendEmail(email, "Informação da Denúncia", message);

            return RedirectToAction("Index", "Home");// e volta apagina inicial
        }

        public ActionResult ShowPDF(Guid id) // formato para pdf
        {

            //cria um obj para enviar para view que ira criar o pdf
            Complaint complaint = new()
            {
                ComplaintId = id,
                PassWord = _complaintRepository.GetComplaintPassWord(id)
            };



            //chama a view que sera o formato do pdf
            return new ViewAsPdf("~/Views/Complaint/ShowPDF.cshtml", complaint)
            {
                FileName = id.ToString() + "|" + DateTime.Now.ToString("ddMMyyyy") + ".pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = { Left = 15, Right = 15, Top = 15 },



            };
        }

        public IActionResult InfoTypeComplaint() // tipos de denuncias
        {

            return View();
        }






    }
}






