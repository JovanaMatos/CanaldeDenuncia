using Projetos_App1.ViewModels;

namespace Projetos_App1.Models.Services.Interfaces
{
    public interface IComplaintService
    {
        Guid Savecomplaint( ComplaintViewModel complaintViewModel);
    }
}
