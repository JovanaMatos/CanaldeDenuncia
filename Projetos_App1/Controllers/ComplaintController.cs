using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ComplaintController(IComplaintRepository complaintRepository,
                                   ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation,
                                   ICompanyRepository companyRepository, IAttachedFileRepository attachedFile, IComplaintService complaintService,
                                   IWhistleblowingService whistleblowingService, IAttachedFileService attachedFileService)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
            _attachedFileRepository = attachedFile;
            _complaintService = complaintService;
            _whistleblowingService = whistleblowingService;
            _attacheFileService = attachedFileService;

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

        public JsonResult GetCategoryByID(int companyId)
        {
            var categories = _categoryRepository.GetCategoryByID(companyId)
                .Select(c => new
                {
                    categoryId = c.CategoryId, 
                    categories = c.Categories
                }).ToList();
            return Json(categories);
        }

        //tratar id

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

        public IActionResult ShowPDF()
        {
            return new ViewAsPdf("ShowPDF");
        }


    }
}






