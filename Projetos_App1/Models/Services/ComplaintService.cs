using Projetos_App1.Models.Repositories;
using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;
using Projetos_App1.ViewModels;

namespace Projetos_App1.Models.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly ICompaniesCategoryRepository _companiesCategoryRepository;

        public ComplaintService(IComplaintRepository complaintRepository, ICompaniesCategoryRepository companiesCategoryRepository)
        {
            _complaintRepository = complaintRepository;
            _companiesCategoryRepository = companiesCategoryRepository;
        }

        public Guid Savecomplaint(ComplaintViewModel complaintViewModel)
        {
            Complaint complaint = new Complaint()
            {
                ComplaintSubject = complaintViewModel.ComplaintSubject,
                ComplaintDescription = complaintViewModel.ComplaintDescription,
                CompanyRelationId = complaintViewModel.CompanyRelationId,
                CompaniesCategoryId = _companiesCategoryRepository.GetCompaniesCategoryById(complaintViewModel.companyid, complaintViewModel.categoryid),
                ShippingMethodsId = 1,
                Complaint_privacy_type = true,// validar verificar
                ComplaintStartDate = DateTime.Now,
                ComplaintStatusId = 1 //id recebida

            };

            complaint.ComplaintId = complaint.CreateId();
            complaint.PassWord = complaint.CreatePassWord();
           

            _complaintRepository.SaveNewComplaint(complaint);

            return complaint.ComplaintId;

        }

    
    }
}
