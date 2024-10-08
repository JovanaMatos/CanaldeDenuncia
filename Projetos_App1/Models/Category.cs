using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Categories { get; set; } = null!;

    public virtual ICollection<CompaniesCategory> CompaniesCategories { get; set; } = new List<CompaniesCategory>();
}
