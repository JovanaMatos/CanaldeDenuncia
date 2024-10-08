using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projetos_App1.Models;

public partial class Complaint
{
    public string ComplaintId { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public bool ComplaintType { get; set; }


    [Display(Name = "Assunto")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "O {0} deve ter no minimo {1} e no máximo {2}")]
    public string ComplaintSubject { get; set; } = null!;

    [Display(Name = "Descrição")]
    [MinLength(20, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
    public string ComplaintDescription { get; set; } = null!;

    public string? ComplaintResponse { get; set; }

    public DateTime ComplaintStartDate { get; set; }

    public DateTime? ComplaintCloseDate { get; set; }


    [Display(Name = "Categoria | Empresa")]

    public int CompaniesCategoryId { get; set; }

    public int ShippingMethodsId { get; set; }

    public int ComplaintStatusId { get; set; }

    [Display(Name = "Relação com a empresa")]
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
}
