using Microsoft.EntityFrameworkCore;
using Projetos_App1.Models.Repositories.Interfaces;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;

namespace Projetos_App1.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ICompaniesCategoryRepository _companiesCategoryRepository;

        public CategoryRepository(AppDbContext context, ICompaniesCategoryRepository companiesCategoryRepository)
        {
            _context = context;
            _companiesCategoryRepository = companiesCategoryRepository;
        }

        // retornando uma lista de category para tipo classe Category
        public IEnumerable<Category> Categories { get => _context.Categories; }


        //busca as categorias da empresa especifica
        public List<Category> GetCategoryByID(int companyID)
        {
            //busca todas as CompaniesCategorias
            var companiesCategories = _context.CompaniesCategories;

           // um join na categorias que existe na companyCategoria
           // e onde company id == companyCatehory
            var queryCategory = _context.Categories.Join(companiesCategories,
                                                     category => category.CategoryId, compCategory => compCategory.CategoryId,
                                                     (category, compCategory) => new { category, compCategory })//guardando var e finalizando join
                                                     .Where(x => x.compCategory.CompaniesId == companyID)
                                                     .Select(x => x.category)
                                                     .ToList();

            return queryCategory;
        }

        // função q encontra o nome da categoria relaciona a empresa
        public async Task<string> GetCategoryByIdCompaniesCategory(int companyCategoryId)
        {
  
            // // busca a empresa e categoria baseado id 
            var companiesCategories = await _companiesCategoryRepository.SearchCompanyCategoryByID(companyCategoryId);

            // busca a  o nome da categoria onde categoriaid de companycategory é igual
            var categoryName = await _context.Categories
                .Where(category => companiesCategories.Select(cc => cc.CategoryId).Contains(category.CategoryId)) // Filtra as categorias diretamente
                .Select(category => category.Categories) //nome da categoria
                .FirstOrDefaultAsync(); // Obtém o primeiro nome da categoria ou nulo

            return categoryName;






        }
    }
}
