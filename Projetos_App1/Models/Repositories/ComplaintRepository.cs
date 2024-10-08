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

        public IEnumerable<Complaint> Complaints => _context.Complaints;//todos

        public Complaint GetComplaintById(string id)//especifico
        {
            return _context.Complaints.Include(c => c.CompaniesCategory)
                                      .Include(c => c.CompanyRelation)
                                      .Include(c => c.ComplaintStatus)
                                      .Include(c => c.CurrentResponsible)
                                      .Include(c => c.ShippingMethods)
                                      .FirstOrDefault(x => x.ComplaintId == id);

        }
    }
}
