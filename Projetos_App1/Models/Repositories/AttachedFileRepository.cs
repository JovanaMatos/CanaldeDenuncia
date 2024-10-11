using Projetos_App1.Models.Repositories.Interfaces;

namespace Projetos_App1.Models.Repositories
{
    public class AttachedFileRepository : IAttachedFileRepository
    {

        private readonly AppDbContext _context;

        public AttachedFileRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<AttachedFile> AttachedFiles => _context.AttachedFiles;

        public void AddAttachedFiles(AttachedFile attachedFiles)
        {
                         
                _context.AttachedFiles.Add(attachedFiles);
                _context.SaveChanges();
            
                
        }
    }
}
