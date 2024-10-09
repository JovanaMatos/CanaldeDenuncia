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
    }
}
