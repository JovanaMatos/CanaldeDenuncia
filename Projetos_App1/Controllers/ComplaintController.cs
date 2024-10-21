using Microsoft.AspNetCore.Mvc;
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
        private readonly IAttachedFileRepository _attachedFileRepository;
        private readonly IComplaintService _complaintService;
        private readonly IWhistleblowingService _whistleblowingService;
        private readonly IAttachedFileService _attacheFileService;
        private readonly IEmail _email;

        public ComplaintController(IComplaintRepository complaintRepository,
                                   ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation,
                                   ICompanyRepository companyRepository, IAttachedFileRepository attachedFile, IComplaintService complaintService,
                                   IWhistleblowingService whistleblowingService, IAttachedFileService attachedFileService, IEmail email)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
            _attachedFileRepository = attachedFile;
            _complaintService = complaintService;
            _whistleblowingService = whistleblowingService;
            _attacheFileService = attachedFileService;
            _email = email;

        }

        [HttpGet]
        public IActionResult CreateComplaint()
        {
            var listcompanies = _companyRepository.companies.ToList();
            var listcategories = _categoryRepository.Categories.ToList();
            var listrelationship = _companyRelationRepository.companyRelations.ToList();

            var complainVm = new ComplaintViewModel()
            {
                listRelation = listrelationship,
                listCompany = listcompanies
            }
            ;

            return View(complainVm);
        }

        public JsonResult GetCategoryByID(int companyId)// para dropdown 
        {
            var categories = _categoryRepository.GetCategoryByID(companyId)
                .Select(c => new
                {
                    categoryId = c.CategoryId, 
                    categories = c.Categories
                }).ToList();
            return Json(categories);
        }

      

        [HttpPost]
        public IActionResult CreateComplaint(ComplaintViewModel complaintVm)
        {

            // Verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                
                return View(complaintVm);
            }



            Complaint complaint = _complaintService.SaveComplaint(complaintVm);//Retorna para pode add em outras tabelas


            if (complaintVm.Name != null)//verifca se é confidencial
            {
                _whistleblowingService.SaveWhistleblowing(complaintVm, complaint.ComplaintId);
            }

            if (complaintVm._files != null && complaintVm._files.Count > 0)//verifica se existe arquivo para verificar
            {
                List<AttachedFile> attachedFiles = _attacheFileService.UploadImg(complaintVm._files);
                _attacheFileService.SaveAttachedFile(attachedFiles, complaint.ComplaintId);
            }



            return View("Views/Complaint/ShowLogin.cshtml", complaint );
        }
       
         public IActionResult ShowLogin()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult ShowLogin(Complaint complaint , string email)
        {

            var teste = $"Seu login {complaint.ComplaintId} e sua senha senha  {complaint.ComplaintId}";
            _email.SendEmail(email, "Sistema de Denúncias", teste);

            //if (sendEmail) { 
            //}

            return RedirectToAction("Index", "Home");
        }




        public ActionResult ShowPDF(Guid id)
        {
       

            Complaint complaint = new Complaint
            {
                ComplaintId = id,
                PassWord = _complaintRepository.GetComplaintPassWord(id)
            };




            return new ViewAsPdf("~/Views/Complaint/ShowPDF.cshtml" , complaint)
            {
                FileName = id.ToString() + "|" + DateTime.Now.ToString("ddMMyyyy") + ".pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = { Left = 15, Right = 15, Top = 15 },
                


            };
        }





    }
}






