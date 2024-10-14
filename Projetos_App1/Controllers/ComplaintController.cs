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
            var listcompanies = _companyRepository.companies.ToList();
            var listcategories = _categoryRepository.GetCategory();
            var listrelationship = _companyRelationRepository.companyRelations.ToList();

            var complainVm = new ComplaintViewModel() {
                listCategory = listcategories,
                listRelation = listrelationship,
                listCompany = listcompanies}
            ;


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
                return View(complaintVm); //Retorna à view com mensagens de validação
            }

            //função para converter tipo model em complaint
            Complaint complaint = complaintVm.ChangeTocomplaint(complaintVm);

            complaint.ComplaintId = complaint.CreateId();//criando id

            // buscandoa relação de Compny e Category
            complaint.CompaniesCategoryId = _companiesCategoryRepository.GetCompaniesCategoryById(complaintVm.companyid, complaintVm.categoryid);

            _complaintRepository.SaveNewComplaint(complaint);

            if (complaintVm.Name != null)
            {
                Whistleblowing whistleblowing = complaintVm.ChangeToWhistleblowing(complaintVm);
                whistleblowing.ComplaintId = complaint.ComplaintId;
                _whistleblowingRepository.SaveWhistleblowing(whistleblowing);
            }

            if (complaintVm._files != null && complaintVm._files.Count > 0)
            {
                // devolve uma lista de AttachedFile
                var attachedFiles = complaintVm.UploadImg(complaintVm._files);

                foreach (var newAttachedFile in attachedFiles)
                {
                    newAttachedFile.ComplaintId = complaint.ComplaintId;
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
