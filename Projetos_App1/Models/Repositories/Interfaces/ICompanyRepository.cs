namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> companies { get; }
        Task<string> GetCompanyByIdCompaniesCategory(int companyCategoryId);
    }
}
