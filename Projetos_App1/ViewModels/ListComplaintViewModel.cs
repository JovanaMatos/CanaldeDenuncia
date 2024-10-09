using Projetos_App1.Models;

namespace Projetos_App1.ViewModels
{
    public class ListComplaintViewModel
    {
        public Complaint? complaint { get; set; }
        public IEnumerable <Category> category { get; set; }

        public IEnumerable <Company?> company { get; set; }

        public IEnumerable<CompanyRelation?> relation { get; set; }   

        public Whistleblowing? Whistleblowing { get; set; }


    }
}
