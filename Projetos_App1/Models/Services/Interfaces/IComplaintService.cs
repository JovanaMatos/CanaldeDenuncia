using Projetos_App1.ViewModels;

namespace Projetos_App1.Models.Services.Interfaces
{
    public interface IComplaintService
    {
        Complaint SaveComplaint(ComplaintViewModel complaintViewModel);
        Task Login(LoginComplaintViewModel complaint);

        Task Logoff();

    }
}
