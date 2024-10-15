using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projetos_App1.Models;
using Projetos_App1.Models.Repositories;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;


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
                                   ICompanyRepository companyRepository,  IAttachedFileRepository attachedFile, IComplaintService complaintService,
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
                //listCategory = listcategories,
                listRelation = listrelationship,
                listCompany = listcompanies
            }
            ;

    
            complainVm.listCategory.Add(new Category()
            {
                CategoryId = 0,
                Categories = "Categorias"
            });

           // ViewBag.listCompany = new SelectList(listcompanies, "CompaniesId", "Name");
           //ViewBag.listCategory = new SelectList(listcategories, "CategoryId", "Categories");

            return View(complainVm);
        }

        public JsonResult GetCategoryByID(int companyId) { 
            return Json(_categoryRepository.GetCategoryByID(companyId));
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

            
            Guid complaintID = _complaintService.SaveComplaint(complaintVm);//Retorna para pode add em outras tabelas


            if (complaintVm.Name != null)
            {
                 _whistleblowingService.SaveWhistleblowing(complaintVm, complaintID);
            }

            if (complaintVm._files != null && complaintVm._files.Count > 0)
            {
                List<AttachedFile> attachedFiles = _attacheFileService.UploadImg(complaintVm._files);
                _attacheFileService.SaveAttachedFile(attachedFiles, complaintID);  
            }



            return RedirectToAction("List", "Complaint");
        }

        public IActionResult List()
        {
            var complaint = _complaintRepository.Complaints;

            return View(complaint);
        }
        public IActionResult ListCat()
        {
            var category = _categoryRepository.Categories;

            return View(category);
        }
    }
}






//Random randNum = new Random();
//var id = randNum.Next(1000).ToString();

//complaint.ComplaintId = id;
