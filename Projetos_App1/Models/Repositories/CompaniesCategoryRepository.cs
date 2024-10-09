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

       

        public CompaniesCategory GetCompaniesCategory(int companyId)
        {
            
            throw new NotImplementedException();
        }

     
    }
}
