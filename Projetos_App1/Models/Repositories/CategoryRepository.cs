using Projetos_App1.Models.Repositories.Interfaces;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;

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


        public List<Category> GetCategory()
        {
            int companyId = 1;
            var companiesCategories = _context.CompaniesCategories;


            //ele faz um join onde existe as categorias no companyCategories e depois com a condição de mostrar
            //apenas onde campanies.id seja igual ao id que vem
            var queryCategory = _context.Categories.Join(companiesCategories,
                                                     category => category.CategoryId, compCategory => compCategory.CategoryId,
                                                     (category, compCategory) => new { category, compCategory })//guardando var e finalizando join
                                                     .Where(x => x.compCategory.CompaniesId == companyId)
                                                     .Select(x => x.category)
                                                     .ToList();

            return queryCategory;
        }
    }
}
