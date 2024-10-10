using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Models.Repositories
{
    public class CompaniesCategoryRepository :  ICompaniesCategoryRepository
    {
        private readonly AppDbContext _context;

        public CompaniesCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

       

        public int GetCompaniesCategoryById(int companyId, int categoryId)
        {

            var companyCategoryId = _context.CompaniesCategories.Where(cc => cc.CompaniesId.Equals(companyId) && cc.CategoryId.Equals(categoryId))
                                                                 .Select(cc => cc.CompaniesCategoryId)
                                                                 .FirstOrDefault();
            return companyCategoryId;
        }

     
    }
}
