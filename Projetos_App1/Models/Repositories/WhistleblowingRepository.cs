using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Models.Repositories
{
    public class WhistleblowingRepository : IWhistleblowingRepository
    {
        public readonly AppDbContext _context;

        public WhistleblowingRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Whistleblowing> whistleblowings => _context.Whistleblowings;

        // salva Whistleblowing
        public void SaveWhistleblowing(Whistleblowing whistleblowing)
        {
            _context.Whistleblowings.Add(whistleblowing);
            _context.SaveChanges();
        }
    }
}
