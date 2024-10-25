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


        public void SaveNewComplaint(Complaint complaint)
        {



            _context.Complaints.Add(complaint); // salvando denuncia em bd
            _context.SaveChanges();
        }

        public async Task<Complaint> GetComplaintByIdAsync(Guid id) // buscar por id de forma assíncrona
        {
            return await _context.Complaints.Include(c => c.CompaniesCategory)
                                              .Include(c => c.CompanyRelation)
                                              .Include(c => c.ComplaintStatus)
                                              .Include(c => c.CurrentResponsible)
                                              .Include(c => c.ShippingMethods)
                                              .FirstOrDefaultAsync(x => x.ComplaintId == id);
        }
        public Guid FindComplaintId(Guid id)
        {

            return _context.Complaints.Where(x => x.ComplaintId == id).Select(x => x.ComplaintId).FirstOrDefault();
        }

        public string GetComplaintPassWord(Guid id)
        {

            var pass = _context.Complaints.Where(x => x.ComplaintId == id)
                                      .Select(p => p.PassWord).FirstOrDefault();
            return pass;
        }



        public async Task<bool> PasswordExists(string password)
        {
            var complaint = await _context.Complaints.FirstOrDefaultAsync(x => x.PassWord.Equals(password));
            return complaint != null;
        }

        public async Task<Complaint> UserExists(Guid id, string password)
        {
            var complaint = await _context.Complaints.Include(c => c.CompaniesCategory)
                                               .Include(c => c.CompanyRelation)
                                               .Include(c => c.ComplaintStatus)
                                               .Include(c => c.CurrentResponsible)
                                               .Include(c => c.ShippingMethods).Where(x => x.ComplaintId.Equals(id) && x.PassWord.Equals(password))
                                               .FirstOrDefaultAsync();

            return complaint;


        }




    }
}
