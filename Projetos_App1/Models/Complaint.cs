
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projetos_App1.Models;

public partial class Complaint
{
    [Required(ErrorMessage = "Campo obrigatório")]
    public Guid ComplaintId { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    public string PassWord { get; set; } = null!;

    public bool Complaint_Is_Confidential { get; set; }



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

    public virtual ICollection<ResponsibleHistory> ResposibleHistories { get; set; } = new List<ResponsibleHistory>();

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
       // var passwordHash = HashPassword(passWord);
        return passWord;
    }

    //por decidir incriptar ou não

    //public string HashPassword(string password)
    //{
    //    return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
    //}

    //// Método para verificar se a senha informada corresponde ao hash
    //public bool VerifyPassword(string password, string passwordHash)
    //{
    //    return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

