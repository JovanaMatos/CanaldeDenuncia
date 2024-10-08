namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        Category GetCategory(string id);
    }
}
