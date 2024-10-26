using Humanizer;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projetos_App1.Models.Repositories.Interfaces;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;

namespace Projetos_App1.Models.Repositories
{
    public class CompaniesCategoryRepository : ICompaniesCategoryRepository
    {
        private readonly AppDbContext _context;

        public CompaniesCategoryRepository(AppDbContext context)
        {
            _context = context;
        }



        public int GetCategoryIdByIdCompaniesCategory(int companyId, int categoryId)
        {

            var companyCategoryId = _context.CompaniesCategories.Where(cc => cc.CompaniesId.Equals(companyId) && cc.CategoryId.Equals(categoryId))
                                                                 .Select(cc => cc.CompaniesCategoryId)
                                                                 .FirstOrDefault();
            return companyCategoryId;
        }

        public async Task<List<CompaniesCategory>> SearchCompanyCategoryByID(int newCompaniesCategoryID)
        {
           // aqui buscoa a empresa e categoria baseado id 
            var companiesCategories = await _context.CompaniesCategories
                .Where(cc => cc.CompaniesCategoryId == newCompaniesCategoryID)
                .ToListAsync();

            
            return companiesCategories;
        }





    }


    //.Join(_context.Categories,
    //             compCategory => compCategory.compCategory.CategoryId,
    //             category => category.CategoryId,
    //             (compCategory, category) => new
    //             {
    //                 CompanyName = compCategory.company.Name,       
    //                 CategoryName = category.Categories,                

    //             })
    //       .ToListAsync();



    ////ele faz um join onde existe as categorias no companyCategories e depois com a condição de mostrar
    ////apenas onde campanies.id seja igual ao id que vem
    //var queryCategory = _context.Categories.Join(companiesCategories,
    //                                         category => category.CategoryId, compCategory => compCategory.CategoryId,
    //                                         (category, compCategory) => new { category, compCategory })//guardando var e finalizando join
    //                                         .Where(x => x.compCategory.CompaniesId == companyID)
    //                                         .Select(x => x.category)
    //                                         .ToList();











    //        ar query = students.Join(departments,
    //    student => student.DepartmentID, department => department.ID,
    //    (student, department) => new { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name });

    //foreach (var item in query)
    //{
    //    Console.WriteLine($"{item.Name} - {item.DepartmentName}");
    //}




    //        SELECT
    //        cc.idcompanyCategory,
    //    c.companyName,  -- substitua pelo nome da coluna que representa o nome da empresa
    //        cat.categoryName -- substitua pelo nome da coluna que representa o nome da categoria
    //        FROM
    //    companyCategory cc
    //JOIN
    //        company c ON cc.companyId = c.id-- substitua 'companyId' e 'id' pelos nomes reais das colunas
    //        JOIN
    //    category cat ON cc.categoryId = cat.id-- substitua 'categoryId' e 'id' pelos nomes reais das colunas
    //WHERE
    //    cc.idcompanyCategory = < seu_id_aqui >;

}

