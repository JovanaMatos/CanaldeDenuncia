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

        // para separar view model do complaint
        public Complaint SaveComplaint(ComplaintViewModel complaintViewModel)
        {
            Complaint complaint = new Complaint()
            {
                ComplaintSubject = complaintViewModel.ComplaintSubject,
                ComplaintDescription = complaintViewModel.ComplaintDescription,
                CompanyRelationId = complaintViewModel.CompanyRelationId,
                CompaniesCategoryId = _companiesCategoryRepository.GetCategoryIdByIdCompaniesCategory(complaintViewModel.companyid, complaintViewModel.categoryid),
                ShippingMethodsId = 1,//metodo de envio 
                ComplaintStartDate = DateTime.Now,
                ComplaintStatusId = 1 //id recebida

            };
            
            if (complaintViewModel.Complaint_Is_Confidential == 1)// verifica se é confidencial
            {
                Console.WriteLine(complaintViewModel.Complaint_Is_Confidential);
                complaint.Complaint_Is_Confidential = true;
            }
            else
            {
                complaint.Complaint_Is_Confidential = false;
            }

            complaint.ComplaintId = complaint.CreateId(); // criando id
            complaint.PassWord = complaint.CreatePassWord(); // acriando pass

            _complaintRepository.SaveNewComplaint(complaint); // salva bd


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

        // finaliza sessão
        public async Task Logoff()
        {
            var ctx = _httpContextAccessor.HttpContext;
            await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }



    }
}
