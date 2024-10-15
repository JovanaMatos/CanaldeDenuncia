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
                //listCategory = listcategories,
                listRelation = listrelationship,
                listCompany = listcompanies
            }
            ;


            //complainVm.listCategory.Add(new Category()
            //{
            //    CategoryId = 0,
            //    Categories = "Categorias"
            //});

            // ViewBag.listCompany = new SelectList(listcompanies, "CompaniesId", "Name");
            //ViewBag.listCategory = new SelectList(listcategories, "CategoryId", "Categories");

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

            //// Verifica se o modelo é válido
            //if (!ModelState.IsValid)
            //{
            //    Console.WriteLine($"Category ID: {complaintVm.categoryid}"); 
            //    return View(complaintVm);
            //}

            if (!ModelState.IsValid)
            {
                // Aqui você pode logar os erros
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log dos erros
                }
                // Retornar a view com os dados já preenchidos

                Console.WriteLine("valor "+ complaintVm.CompanyRelationId);
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
