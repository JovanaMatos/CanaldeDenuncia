using System.ComponentModel.DataAnnotations;

namespace Projetos_App1.ViewModels
{
    public class AccessComplaintViewModel   // para acessar denuncia
    {


        public string ComplaintSubject { get; set; } = null!;
        public string Company { get; set; }
        public string Category { get; set; }
        public string ComplaintDescription { get; set; } = null!;
        public string ComplaintStatus { get; set; }

        public string? CompanyRelation { get; set; }
        public List<string>? _filesName { get; set; } = new List<string>();
        

       








    }
}
