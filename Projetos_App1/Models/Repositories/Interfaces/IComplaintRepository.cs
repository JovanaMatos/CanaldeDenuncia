namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface IComplaintRepository
    {
        IEnumerable<Complaint> Complaints { get; }

        Complaint GetComplaintById(string id);

    }
}
