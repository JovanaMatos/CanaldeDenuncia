using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Projetos_App1.Models;

namespace Projetos_App1.ViewModels
{
    public class ComplaintViewModel
    {
        public Complaint? complaint { get; set; }

        public int categoryid { get; set; }

        public int companyid { get; set; }
        public int? companyRelationid { get; set; }
        public IEnumerable <Category> listCategory { get; set; }

        public IEnumerable <Company> listCompany { get; set; }

        public IEnumerable<CompanyRelation> listRelation { get; set; }   

        public Whistleblowing? Whistleblowing { get; set; }

        public IEnumerable<AttachedFile> attachedFiles { get; set; }


       
    }
}
