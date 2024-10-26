namespace Projetos_App1.Models.Repositories.Interfaces
{
    public interface ICompaniesCategoryRepository
    {
   

        int GetCategoryIdByIdCompaniesCategory(int companyId, int categoryId);
        Task<List<CompaniesCategory>> SearchCompanyCategoryByID(int CompaniesCategoryiD);
    }
}
