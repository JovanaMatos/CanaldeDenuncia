namespace Projetos_App1.Models.Services.Interfaces
{
    public interface IAttachedFileService 
    {
        List<AttachedFile> UploadImg(IList<IFormFile> attachedFileVM);
        void SaveAttachedFile(List<AttachedFile> attachedFilesList, Guid complaintID);
    }
}
