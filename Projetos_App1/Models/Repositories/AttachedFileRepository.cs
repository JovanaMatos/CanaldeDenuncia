using Microsoft.EntityFrameworkCore;
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

       //adicionando arquivos
        public void AddAttachedFiles(AttachedFile attachedFiles)
        {
                         
                _context.AttachedFiles.Add(attachedFiles);
                _context.SaveChanges();
            
                
        }
        //buscando arquivos
        public async Task<List<string>> ListAttachedFile(Guid IdComplaint)
        {
            var listAttachedFile = await _context.AttachedFiles.Where(at => at.ComplaintId.Equals(IdComplaint)).Select(f => f.FilesName).ToListAsync();

            return listAttachedFile;
        }
    }
}
