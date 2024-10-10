namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface IWhistleblowingRepository
    {
        IEnumerable<Whistleblowing> whistleblowings { get; }

        void SaveWhistleblowing (Whistleblowing whistleblowing);
    }
}
