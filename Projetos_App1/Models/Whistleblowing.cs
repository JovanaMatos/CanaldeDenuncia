using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projetos_App1.Models;

public partial class Whistleblowing
{

    public int WhistleblowingId { get; set; }

    [Display(Name = "Nome")]
    public string Name { get; set; } = null!;

    public string? Adress { get; set; }

    [EmailAddress(ErrorMessage = "Fomato de email, invalido!")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Phone]
    [Display(Name = "Telemovel")]
    public string? PhoneNumber { get; set; }

    public Guid ComplaintId { get; set; } 

    public virtual Complaint Complaint { get; set; } = null!;

}
