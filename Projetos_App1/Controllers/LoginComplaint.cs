
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projetos_App1.Models;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;
using System.Security.Claims;


namespace Projetos_App1.Controllers
{
    public class LoginComplaint : Controller
    {
        private readonly IComplaintService _complaintService;
        private readonly IComplaintRepository _complaintRepository;

        public LoginComplaint(IComplaintService complaintService, IComplaintRepository complaintRepository)
        {
            _complaintService = complaintService;
            _complaintRepository = complaintRepository;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {

        
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> AccessComplaint(LoginComplaintViewModel complaintVm)
        {
          


            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", complaintVm);
            }
       
            Guid.TryParse(complaintVm.ComplaintId, out Guid id);

            var IdcomplaintExists = await _complaintRepository.GetComplaintByIdAsync(id);

            // verifica se id existe e senha
            if (IdcomplaintExists == null || !await _complaintRepository.PasswordExists(complaintVm.PassWord))
            {
                ViewData["LoginError"] = "Credenciais inválidas.";
                return RedirectToAction(nameof(Login));
                
                //mandar erro
            }

            // se sim busca complaint de id senha igual

            var complantNew = await _complaintRepository.UserExists(id, complaintVm.PassWord);

            if (complantNew == null)
            {
                TempData["LoginError"] = "Credenciais inválidas.";
                return RedirectToAction(nameof(Login));
            }
            else
            {
                await _complaintService.Login(complaintVm);

                return View("Views\\LoginComplaint\\Details.cshtml", complantNew);
            }

        }

        [Authorize]
        public async Task<ActionResult> Logoff()
        {
            await _complaintService.Logoff();
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public ActionResult Details()
        {
            //ViewBag.Permits = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name)
            return View();
        }

      

    }
}
