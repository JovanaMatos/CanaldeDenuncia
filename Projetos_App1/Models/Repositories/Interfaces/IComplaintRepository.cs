namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface IComplaintRepository
    {
        IEnumerable<Complaint> Complaints { get; }

        void SaveNewComplaint(Complaint complaint);
        Task<Complaint> GetComplaintByIdAsync(Guid id);
        Guid FindComplaintId(Guid id);
        string GetComplaintPassWord(Guid id);
        Task<bool> PasswordExists(string password);

        Task<Complaint> UserExists(Guid id, string password);

    }
}
