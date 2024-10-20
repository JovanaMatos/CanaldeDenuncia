namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface IComplaintRepository
    {
        IEnumerable<Complaint> Complaints { get; }

        void SaveNewComplaint(Complaint complaint);
        Complaint GetComplaintById(Guid id);
        Guid FindComplaintId(Guid id);
        string GetComplaintPassWord(Guid id);

    }
}
