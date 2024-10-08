using Microsoft.AspNetCore.Mvc;
using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;

        public ComplaintController(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public IActionResult List()
        {
            var complaint = _complaintRepository.Complaints;
            return View(complaint);
        }
    }
}
