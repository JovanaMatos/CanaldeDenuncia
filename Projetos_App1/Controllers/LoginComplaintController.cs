
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projetos_App1.Models;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;
using System.Security.Claims;


namespace Projetos_App1.Controllers
{
    public class LoginComplaintController : Controller
    {
        private readonly IComplaintService _complaintService;
        private readonly IComplaintRepository _complaintRepository;
        private readonly ICompanyRelationRepository _companyRelationRepository;
        private readonly IAttachedFileRepository _attachedFileRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICategoryRepository _categoryRepository;

        public LoginComplaintController(IComplaintService complaintService, IComplaintRepository complaintRepository, ICompanyRelationRepository companyRelationRepository, ICompanyRepository companyRepository, ICategoryRepository categoryRepository, IAttachedFileRepository attachedFileRepository)
        {
            _complaintService = complaintService;
            _complaintRepository = complaintRepository;
            _companyRelationRepository = companyRelationRepository;
            _attachedFileRepository = attachedFileRepository;
            _companyRepository = companyRepository;
            _categoryRepository = categoryRepository;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {

        
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login(LoginComplaintViewModel complaintVm)
        {

            if (!ModelState.IsValid)
            {
                ViewData["LoginError"] = "Por favor, corrija os erros.";
                return View(complaintVm); // Altere para retornar a View em vez de RedirectToAction
            }


            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Login", complaintVm);
            //}
       
            Guid.TryParse(complaintVm.ComplaintId, out Guid id);

            var IdcomplaintExists = await _complaintRepository.GetComplaintByIdAsync(id);

            // verifica se id existe e senha
            if (IdcomplaintExists == null || !await _complaintRepository.PasswordExists(complaintVm.PassWord))
            {
                ViewData["LoginError"] = "Credenciais inválidas.";
                return View(complaintVm);
                
                //mandar erro
            }

            // se sim busca complaint de id senha igual

            var complantNew = await _complaintRepository.UserExists(id, complaintVm.PassWord);

            if (complantNew == null)
            {
                ViewData["LoginError"] = "Credenciais inválidas.";
                return View(complaintVm);
            }
            else
            {
                await _complaintService.Login(complaintVm);

                return RedirectToAction("DetailsComplaint", "LoginComplaint", complantNew);
            }

        }

        [Authorize]
        public async Task<ActionResult> Logoff()
        {
            await _complaintService.Logoff();
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public async Task <ActionResult> DetailsComplaint(Complaint complant)
        {
            AccessComplaintViewModel accessComplaint = new AccessComplaintViewModel()
            {
                ComplaintSubject = complant.ComplaintSubject,
                ComplaintDescription = complant.ComplaintDescription,
                Company = await _companyRepository.GetCompanyByIdCompaniesCategory(complant.CompaniesCategoryId),
                Category = await _categoryRepository.GetCategoryByIdCompaniesCategory(complant.CompaniesCategoryId),
                ComplaintStatus = "Recebida"//fazer pequisa
                


            };
            if (complant.CompanyRelationId != null)
            {
                accessComplaint.CompanyRelation = await _companyRelationRepository.CompanyRelation(complant.CompanyRelationId.Value);
            }

            if (complant.AttachedFiles != null) {

               accessComplaint._filesName.AddRange(await _attachedFileRepository.ListAttachedFile(complant.ComplaintId));

            }
          

           

            return View(accessComplaint);
        }

      

    }
}
