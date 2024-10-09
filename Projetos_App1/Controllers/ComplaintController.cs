using Microsoft.AspNetCore.Mvc;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.ViewModels;

namespace Projetos_App1.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRelationRepository _companyRelationRepository;
        private readonly ICompanyRepository _companyRepository;

        public ComplaintController(IComplaintRepository complaintRepository, 
              ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation, ICompanyRepository companyRepository)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
            
        }

        [HttpGet]
        public  IActionResult Index()
        {
            var companies = _companyRepository.companies;
           var categories =  _categoryRepository.GetCategory();
           var relationship = _companyRelationRepository.companyRelations;


            var complainVm = new ListComplaintViewModel
            {
                category = categories,
                relation = relationship,
                company = companies
            };
            
            return View(complainVm);
        }

        [HttpPost]
        public IActionResult CreateComplaint()
        { 


            return View();
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
