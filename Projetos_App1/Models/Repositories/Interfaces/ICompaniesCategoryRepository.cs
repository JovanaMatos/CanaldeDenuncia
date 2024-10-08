namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface ICompaniesCategoryRepository
    {
        IEnumerable<CompaniesCategory> CompaniesCategory { get; }

        CompaniesCategory GetCompaniesCategoryById(int id);
    }
}
