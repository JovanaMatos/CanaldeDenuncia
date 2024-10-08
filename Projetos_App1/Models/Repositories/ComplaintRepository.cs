using Microsoft.EntityFrameworkCore;
using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Models.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly AppDbContext _context;

        public ComplaintRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Complaint> Complaints => _context.Complaints;

        public Complaint GetComplaintById(string id)
        {
            return _context.Complaints.Include(cr => cr.CompanyRelation)
                .Include(cc => cc.CompaniesCategory).Include(a => a.AttachedFiles)
                .Include(cs => cs.ComplaintStatus).FirstOrDefault(x => x.ComplaintId == id);
                
        }
    }
}
