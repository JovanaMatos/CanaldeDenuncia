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

        public async Task<string> GetCompanyByIdCompaniesCategory(int companyCategoryId)
        {
            var companiesCategories = await _companiesCategoryRepository.SearchCompanyCategoryByID(companyCategoryId);


            //var companyName = await _context.Companies
            //.Join(companiesCategories,
            //      company => company.CompaniesId,
            //      compCategory => compCategory.CompaniesId,
            //      (company, compCategory) => company.Name)
            //.FirstOrDefaultAsync();
            //return companyName;


            // Obtenha todos os IDs de empresas das categorias
            var categoryIds = companiesCategories.Select(cc => cc.CompaniesId).ToList();

            // Filtra as empresas usando a lista de IDs
            var companyName = await _context.Companies
                .Where(company => categoryIds.Contains(company.CompaniesId))
                .Select(company => company.Name)
                .FirstOrDefaultAsync(); // Obtém o primeiro nome da empresa ou valor padrão

            return companyName;







        }
    }
}



//// fazendo joins com Companies e Categories 
//var queryCompanyCategory = await _context.Companies
//    .Join(companiesCategories,
//          company => company.CompaniesId,
//          compCategory => compCategory.CompaniesId,
//          (company, compCategory) => new { company, compCategory })
//    .Join(_context.Categories,
//          compCategory => compCategory.compCategory.CategoryId,
//          category => category.CategoryId,
//          (compCategory, category) => new
//          {
//              CompanyName = compCategory.company.Name,
//              CategoryName = category.Categories,  
//          })
//    .ToListAsync();