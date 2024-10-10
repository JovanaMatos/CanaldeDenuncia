namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface IComplaintRepository
    {
        IEnumerable<Complaint> Complaints { get; }

        void SaveComplaint(Complaint complaint);
        Complaint GetComplaintById(string id);

    }
}
