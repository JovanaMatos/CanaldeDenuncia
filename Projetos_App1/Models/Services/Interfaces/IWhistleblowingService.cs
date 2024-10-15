using Projetos_App1.Models;

namespace Projetos_App1.ViewModels
{
    public interface IWhistleblowingService
    {
       Whistleblowing SaveWhistleblowing(ComplaintViewModel complantViewModel, Guid complaintID);
    }
}
