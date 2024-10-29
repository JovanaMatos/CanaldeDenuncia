
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
        public ActionResult Login() // logar
        {


            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login(LoginComplaintViewModel complaintVm)
        {

            if (!ModelState.IsValid)
            {

                return View(complaintVm);
            }



            Guid.TryParse(complaintVm.ComplaintId, out Guid id); // convertendo input string para Guid

            var IdcomplaintExists = await _complaintRepository.GetComplaintByIdAsync(id); // busca pelo id, se existir retorna um complant

            // verifica se id existe e senha
            if (IdcomplaintExists == null || !await _complaintRepository.PasswordExists(complaintVm.PassWord)) // verifica senha existe
            {
                ViewData["LoginError"] = "Credenciais inválidas.";
                return View(complaintVm); //se não retorna erro

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

                return RedirectToAction("DetailsComplaint", new { complaintId = id });//vai para view para mostrar detalhes da denuncia, passando id

            }

        }

        [Authorize]
        public async Task<ActionResult> Logoff()//
        {
            await _complaintService.Logoff(); //fecha sessão
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public async Task<ActionResult> DetailsComplaint(Guid complaintId)
        {

            var complaint = await _complaintRepository.GetComplaintByIdAsync(complaintId);
            if (complaint == null)
            {

                return NotFound();
            }

            AccessComplaintViewModel accessComplaint = new AccessComplaintViewModel()//passando para view model
            {
                ComplaintSubject = complaint.ComplaintSubject,
                ComplaintDescription = complaint.ComplaintDescription,
                Company = await _companyRepository.GetCompanyByIdCompaniesCategory(complaint.CompaniesCategoryId),//função que busca nome da empresa
                Category = await _categoryRepository.GetCategoryByIdCompaniesCategory(complaint.CompaniesCategoryId),//busca nome da categoria
                ComplaintStatus = "Recebida"//fazer pequisa quando implementar gestão


            };

            if (complaint.CompanyRelationId != null)// verifica se é nulo pois é opcional
            {
                //se não, busca relação
                accessComplaint.CompanyRelation = await _companyRelationRepository.CompanyRelation(complaint.CompanyRelationId.Value);
            }

            if (complaint.AttachedFiles != null)// verifica se é nulo pois é opcional
            {
                //se não, busca arquivos
                accessComplaint._filesName.AddRange(await _attachedFileRepository.ListAttachedFile(complaint.ComplaintId));

            }


            return View(accessComplaint);//retorna dados para view
        }



    }
}
