using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.ViewModels;


namespace Projetos_App1.Models.Services
{
    public class WhistleblowingService : IWhistleblowingService
    {
        private readonly IWhistleblowingRepository _repository;

        public WhistleblowingService(IWhistleblowingRepository repository)
        {
            _repository = repository;
        }

        // salvando Whistleblowing
        public Whistleblowing SaveWhistleblowing(ComplaintViewModel complantViewModel, Guid ComplaintID)
        {

            Whistleblowing whistleblowing = new Whistleblowing()
            {

                Name = complantViewModel.Name,
                Email = complantViewModel.Email,
                PhoneNumber = complantViewModel.PhoneNumber,
                ComplaintId = ComplaintID


            };

            _repository.SaveWhistleblowing(whistleblowing);


            return whistleblowing;
           
        }
    }
}
