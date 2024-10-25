using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Projetos_App1.Models.Repositories;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;
using System.Security.Claims;

namespace Projetos_App1.Models.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly ICompaniesCategoryRepository _companiesCategoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ComplaintService(IComplaintRepository complaintRepository, ICompaniesCategoryRepository companiesCategoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _complaintRepository = complaintRepository;
            _companiesCategoryRepository = companiesCategoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Complaint SaveComplaint(ComplaintViewModel complaintViewModel)
        {
            Complaint complaint = new Complaint()
            {
                ComplaintSubject = complaintViewModel.ComplaintSubject,
                ComplaintDescription = complaintViewModel.ComplaintDescription,
                CompanyRelationId = complaintViewModel.CompanyRelationId,
                CompaniesCategoryId = _companiesCategoryRepository.GetCompaniesCategoryById(complaintViewModel.companyid, complaintViewModel.categoryid),
                ShippingMethodsId = 1,
                Complaint_Is_Confidential = true,// validar verificar
                ComplaintStartDate = DateTime.Now,
                ComplaintStatusId = 1 //id recebida

            };

            complaint.ComplaintId = complaint.CreateId();
            complaint.PassWord = complaint.CreatePassWord();


            _complaintRepository.SaveNewComplaint(complaint);



            return complaint;

        }

        public async Task Login(LoginComplaintViewModel complaint)
        {

            var ctx = _httpContextAccessor.HttpContext; // Acesso ao HttpContext

      
            // Criação de claims e autenticação
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, complaint.ComplaintId.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
                IssuedUtc = DateTime.UtcNow
            };

            await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            
        }

        public async Task Logoff()
        {
            var ctx = _httpContextAccessor.HttpContext;
            await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }



    }
}
