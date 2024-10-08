﻿using Projetos_App1.Models.Repositories.Interfaces;

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

      
        Category GetCategory(int id)
        {
            var a = _context.CompaniesCategories;
            var x = _context.Categories.SelectMany(c => c.Categories).Join<.

            throw new NotImplementedException();
        }
    }
}
