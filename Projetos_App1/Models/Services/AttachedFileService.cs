using Projetos_App1.Models.Repositories;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;

namespace Projetos_App1.Models.Services
{
    public class AttachedFileService : IAttachedFileService
    {
        private readonly IAttachedFileRepository _attachedFileRepository;

        public AttachedFileService(IAttachedFileRepository repository)
        {
            _attachedFileRepository = repository;
        }

       

        public List<AttachedFile> UploadImg(IList<IFormFile> attachedFileVM)
        {
            List<AttachedFile> listFiles = new List<AttachedFile>();

            foreach (var file in attachedFileVM)
            {
                // para copiar o conteúdo do arquivo
                using (var ms = new MemoryStream())
                {
                    file.OpenReadStream().CopyTo(ms);

                    AttachedFile attachedFile = new AttachedFile()
                    {
                        FilesName = file.FileName,
                        ImgSize = file.Length,
                        Image = ms.ToArray(),
                        FileType = file.ContentType,
                        SubmissionDate = DateTime.Now
                    };

                    listFiles.Add(attachedFile);
                }
            }

            return listFiles;
        }

        public void SaveAttachedFile(List<AttachedFile> attachedFilesList, Guid complaintID)
        {
            foreach (var newAttachedFile in attachedFilesList)
            {
                newAttachedFile.ComplaintId = complaintID;
                _attachedFileRepository.AddAttachedFiles(newAttachedFile);
            }

        }
    }
}
