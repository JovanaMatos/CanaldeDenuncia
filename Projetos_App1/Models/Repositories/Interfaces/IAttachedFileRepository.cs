namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface IAttachedFileRepository
    {
        IEnumerable<AttachedFile> AttachedFiles { get; }

        void AddAttachedFiles(AttachedFile attachedFiles);

        Task<List<string>> ListAttachedFile(Guid IdComplaint);

    }
}
