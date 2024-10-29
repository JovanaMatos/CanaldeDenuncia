using Microsoft.EntityFrameworkCore;
using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Models.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;
        private readonly ICompaniesCategoryRepository _companiesCategoryRepository;

        public CompanyRepository(AppDbContext context, ICompaniesCategoryRepository categoryRepository)
        {
            _context = context;
            _companiesCategoryRepository = categoryRepository;
        }


        public IEnumerable<Company> companies => _context.Companies;

        // funçao retorna o nome da empresa relacionado a companyCategoryId
        public async Task<string> GetCompanyByIdCompaniesCategory(int companyCategoryId)
        {
            var companiesCategories = await _companiesCategoryRepository.SearchCompanyCategoryByID(companyCategoryId);


            // busca a  o nome da empresa onde companyid de companycategory é igual
            var companyName = await _context.Companies
                .Where(category => companiesCategories.Select(cc => cc.CompaniesId).Contains(category.CompaniesId))
                .Select(company => company.Name)
                .FirstOrDefaultAsync(); 

            return companyName;


        }
    }
}


