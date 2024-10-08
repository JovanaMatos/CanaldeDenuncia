using Projetos_App1.Models.Repositories.Interfaces;
using System.ComponentModel.Design;

namespace Projetos_App1.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        // retornando uma lista de category para tipo classe Category
        public IEnumerable<Category> Categories { get => _context.Categories; }


        public List <Category> GetCategory(int companyId)
        {
           
            var companiesCategories = _context.CompaniesCategories;

            
            var categories = _context.Categories

                .Join(companiesCategories,
                      c => c.CategoryId,  
                      cc => cc.CategoryId, 
                      (c, cc) => new { c, cc }) 
                .Where(result => result.cc.CompaniesId == companyId) 
                .Select(result => result.c) 
                .ToList(); 

            return categories;
        }
    }
}
