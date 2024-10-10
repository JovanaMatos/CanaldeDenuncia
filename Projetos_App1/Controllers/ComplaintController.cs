using Microsoft.AspNetCore.Mvc;
using Projetos_App1.Models;
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
        private readonly ICompaniesCategoryRepository _companiesCategoryRepository;
        private readonly IWhistleblowingRepository _whistleblowingRepository;

        public ComplaintController(IComplaintRepository complaintRepository,
              ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation,
              ICompanyRepository companyRepository, ICompaniesCategoryRepository companiesCategoryRepository,
              IWhistleblowingRepository whistleblowingRepository)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
            _companiesCategoryRepository = companiesCategoryRepository;
            _whistleblowingRepository = whistleblowingRepository;
        }

        [HttpGet]
        public IActionResult CreateComplaint()
        {
            var companies = _companyRepository.companies;
            var categories = _categoryRepository.GetCategory();
            var relationship = _companyRelationRepository.companyRelations;




            var complainVm = new ComplaintViewModel
            {
                listCategory = categories,
                listRelation = relationship,
                listCompany = companies
            };

            return View(complainVm);
        }

        //tratar id


        [HttpPost]
        public IActionResult CreateComplaint(ComplaintViewModel complaintVm)
        {
            if (complaintVm == null)
            {
                return View("Error");
            }
            var id = "kjsnsjfgfisdcefao123456";
            var password = "12osdhuichsuiodhchUIO3456";

            Complaint complaint = new Complaint()
            {
                ComplaintId = id,
                PassWord = password,
                ShippingMethodsId = 1,
                ComplaintType = true,
                ComplaintSubject = complaintVm.complaint.ComplaintSubject,
                ComplaintDescription = complaintVm.complaint.ComplaintDescription,
                ComplaintStartDate = DateTime.Now,
                CompaniesCategoryId = _companiesCategoryRepository.GetCompaniesCategoryById(complaintVm.companyid, complaintVm.categoryid),
                CompanyRelationId = complaintVm.complaint.CompanyRelationId,
                ComplaintStatusId = 1


            };



             _complaintRepository.SaveComplaint(complaint);


             if (complaintVm.Whistleblowing != null)
            {
                   
                    Whistleblowing whistleblowing = new Whistleblowing()
                    {
                        ComplaintId= id,
                        Name = complaintVm.Whistleblowing.Name,
                        Email = complaintVm.Whistleblowing.Email,
                        Adress = complaintVm.Whistleblowing.Adress,
                        PhoneNumber = complaintVm.Whistleblowing.PhoneNumber
                        
                    };
                   
                    _whistleblowingRepository.SaveWhistleblowing(whistleblowing);
            }



            return RedirectToAction( "List", "Complaint");
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
