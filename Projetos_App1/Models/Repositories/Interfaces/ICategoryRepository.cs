namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        List<Category> GetCategoryByID(int companyID);

        Task<string> GetCategoryByIdCompaniesCategory(int companyCategoryId);
    }
}
