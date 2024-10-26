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


        public List<Category> GetCategoryByID(int companyID)
        {
            //int companyId = 1;
            var companiesCategories = _context.CompaniesCategories;


            //ele faz um join onde existe as categorias no companyCategories e depois com a condição de mostrar
            //apenas onde campanies.id seja igual ao id que vem
            var queryCategory = _context.Categories.Join(companiesCategories,
                                                     category => category.CategoryId, compCategory => compCategory.CategoryId,
                                                     (category, compCategory) => new { category, compCategory })//guardando var e finalizando join
                                                     .Where(x => x.compCategory.CompaniesId == companyID)
                                                     .Select(x => x.category)
                                                     .ToList();

            return queryCategory;
        }


        public async Task<string> GetCategoryByIdCompaniesCategory(int companyCategoryId)
        {
            //var companiesCategories = await _companiesCategoryRepository.SearchCompanyCategoryByID(companyCategoryId);


            //var categoryName = await _context.Categories
            //                        .Join(companiesCategories,
            //                                category => category.CategoryId,
            //                                compCategory => compCategory.CompaniesId,
            //                                (category, compCategory) => category.Categories)
            //                        .FirstOrDefaultAsync();

            //return categoryName;
            // Primeiro, busque as categorias relacionadas a esse ID
            var companiesCategories = await _companiesCategoryRepository.SearchCompanyCategoryByID(companyCategoryId);

            // Agora, faça o Join
            var categoryName = await _context.Categories
                .Where(category => companiesCategories.Select(cc => cc.CategoryId).Contains(category.CategoryId)) // Filtra as categorias diretamente
                .Select(category => category.Categories) // Projeta o nome da categoria
                .FirstOrDefaultAsync(); // Obtém o primeiro nome da categoria ou nulo

            return categoryName;






        }
    }
}
