using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projetos_App1.Models;

public partial class Company
{
    public int CompaniesId { get; set; }

    
    public string Name { get; set; }

    public string Niss { get; set; }

    public string Adreess { get; set; }

    public string PhoneNumber { get; set; }

    public virtual ICollection<CompaniesCategory> CompaniesCategories { get; set; } = new List<CompaniesCategory>();
}
