using Projetos_App1.Models.Repositories.Interfaces;
using Projetos_App1.Models.Services.Interfaces;

namespace Projetos_App1.Models.Services
{
    public class WhistleblowingService : IWhistleblowingService
    {
        private readonly IWhistleblowingRepository _repository;

        public WhistleblowingService(IWhistleblowingRepository repository)
        {
            _repository = repository;
        }

        public void SaveWhistleblowing()
        {
            throw new NotImplementedException();
        }
    }
}
