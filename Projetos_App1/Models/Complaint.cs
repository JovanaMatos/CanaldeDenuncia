using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projetos_App1.Models;

public partial class Complaint
{
    public Guid ComplaintId { get; set; } 

    public string PassWord { get; set; } = null!;

    public bool Complaint_privacy_type { get; set; }



    [StringLength(100, MinimumLength = 10, ErrorMessage = "O {0} deve ter no minimo {1} e no máximo {2}")]
    public string ComplaintSubject { get; set; } = null!;

    [MinLength(20, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
    public string ComplaintDescription { get; set; } = null!;

    public string? ComplaintResponse { get; set; }

    public DateTime ComplaintStartDate { get; set; }

    public DateTime? ComplaintCloseDate { get; set; }



    public int CompaniesCategoryId { get; set; }

    public int ShippingMethodsId { get; set; }

    public int ComplaintStatusId { get; set; }

    public int? CompanyRelationId { get; set; }

    public int? CurrentResponsibleId { get; set; }

    public virtual ICollection<AttachedFile> AttachedFiles { get; set; } = new List<AttachedFile>();

    public virtual CompaniesCategory CompaniesCategory { get; set; } = null!;

    public virtual CompanyRelation? CompanyRelation { get; set; }

    public virtual ComplaintStatus ComplaintStatus { get; set; } = null!;

    public virtual Responsible? CurrentResponsible { get; set; }

    public virtual ICollection<ResposibleHistory> ResposibleHistories { get; set; } = new List<ResposibleHistory>();

    public virtual ShippingMethod ShippingMethods { get; set; } = null!;

    public virtual Whistleblowing? Whistleblowing { get; set; }

  
    public Guid CreateId()
    {
       Guid complaintId = Guid.NewGuid();

       return ComplaintId;

    } 
    public string CreatePassWord()
    {
        string passWord = Guid.NewGuid().ToString().Replace("-", "");
       
        return passWord;
    }
}
