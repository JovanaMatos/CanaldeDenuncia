using Microsoft.EntityFrameworkCore;
using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Models.Repositories
{
    public class CompanyRelationRepository : ICompanyRelationRepository
    {
        private readonly AppDbContext _context;

        public CompanyRelationRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CompanyRelation> companyRelations => _context.CompanyRelations;

       public async Task<string> CompanyRelation(int companyRelationId)
        {
            var relation = await _context.CompanyRelations.Where(r => r.CompanyRelationId == companyRelationId).Select(x => x.CompanyRelationship).FirstOrDefaultAsync();
            return relation;
        }
    }
}
