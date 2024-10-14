using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projetos_App1.Models;
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
        private readonly IWhistleblowingRepository _whistleblowingRepository;
        private readonly IAttachedFileRepository _attachedFileRepository;
        private readonly IComplaintService _complaintService;
        public ComplaintController(IComplaintRepository complaintRepository,
                                   ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation,
                                   ICompanyRepository companyRepository, IWhistleblowingRepository whistleblowingRepository, IAttachedFileRepository attachedFile, IComplaintService complaintService)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
            _whistleblowingRepository = whistleblowingRepository;
            _attachedFileRepository = attachedFile;
            _complaintService = complaintService;

        }

        [HttpGet]
        public IActionResult CreateComplaint()
        {
            var listcompanies = _companyRepository.companies.ToList();
            var listcategories = _categoryRepository.GetCategory().ToList();
            var listrelationship = _companyRelationRepository.companyRelations.ToList();

            var complainVm = new ComplaintViewModel()
            {
                listCategory = listcategories,
                listRelation = listrelationship,
                listCompany = listcompanies
            }
            ;

            //ViewBag.listCompany = new SelectList(listcompanies, "CompaniesId", "Name");
            //ViewBag.listCategory = new SelectList(listcategories, "CategoryId", "Categories");

            return View(complainVm);
        }

        //tratar id

        [HttpPost]
        public IActionResult CreateComplaint(ComplaintViewModel complaintVm)
        {

            // Verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                
                return View(complaintVm); //Retorna à view com mensagens de validação
            }

            Guid complaintID = _complaintService.Savecomplaint(complaintVm);


            if (complaintVm.Name != null)
            {
                Whistleblowing whistleblowing = complaintVm.ChangeToWhistleblowing(complaintVm);
                whistleblowing.ComplaintId = complaintID;
                _whistleblowingRepository.SaveWhistleblowing(whistleblowing);
            }

            if (complaintVm._files != null && complaintVm._files.Count > 0)
            {
                // devolve uma lista de AttachedFile
                var attachedFiles = complaintVm.UploadImg(complaintVm._files);

                foreach (var newAttachedFile in attachedFiles)
                {
                    newAttachedFile.ComplaintId = complaintID;
                    _attachedFileRepository.AddAttachedFiles(newAttachedFile);
                }
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
