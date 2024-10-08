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

    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Phone]
    [Display(Name = "Telemovel")]
    public string? PhoneNumber { get; set; }

    public string ComplaintId { get; set; } = null!;

    public virtual Complaint Complaint { get; set; } = null!;
}
