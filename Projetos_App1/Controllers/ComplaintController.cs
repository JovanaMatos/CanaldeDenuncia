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
        private readonly IAttachedFileRepository _attachedFileRepository;

        public ComplaintController(IComplaintRepository complaintRepository,
                                   ICategoryRepository categoryRepository, ICompanyRelationRepository companyRelation,
                                   ICompanyRepository companyRepository, ICompaniesCategoryRepository companiesCategoryRepository,
                                   IWhistleblowingRepository whistleblowingRepository, IAttachedFileRepository attachedFile)
        {
            _complaintRepository = complaintRepository;
            _categoryRepository = categoryRepository;
            _companyRelationRepository = companyRelation;
            _companyRepository = companyRepository;
            _companiesCategoryRepository = companiesCategoryRepository;
            _whistleblowingRepository = whistleblowingRepository;
            _attachedFileRepository = attachedFile;
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

            // Verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelo inválido. Verifique os campos obrigatórios.");
                return View(complaintVm); // Retorna à view com mensagens de validação
            }


            Complaint complaint = complaintVm.ChangeTocomplaint(complaintVm);

            Random randNum = new Random();
            var id2 = randNum.Next(1000);
            var password = "12osdhuichsuiodhchUIO3456";
            var id = Convert.ToString(id2);

            complaint.ComplaintId = id;
            complaint.PassWord = password;
            complaint.ShippingMethodsId = 1;
            complaint.Complaint_privacy_type = true;
            complaint.ComplaintStartDate = DateTime.Now;
            complaint.CompaniesCategoryId = _companiesCategoryRepository.GetCompaniesCategoryById(complaintVm.companyid, complaintVm.categoryid);
            complaint.ComplaintStatusId = 1;

            _complaintRepository.SaveComplaint(complaint);

            if (complaintVm.Name != null)
            {
                Whistleblowing whistleblowing = complaintVm.ChangeToWhistleblowing(complaintVm);
                whistleblowing.ComplaintId = id;

                _whistleblowingRepository.SaveWhistleblowing(whistleblowing);
            }


            if (complaintVm._files != null && complaintVm._files.Count > 0)
            {
                // devolve uma lista de AttachedFile
                var attachedFiles = complaintVm.UploadImg(complaintVm._files);
          
                foreach (var newAttachedFile in attachedFiles)
                {
                    newAttachedFile.ComplaintId = id;
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
