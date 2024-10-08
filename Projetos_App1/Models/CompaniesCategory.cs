using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class CompaniesCategory
{
    public int CompaniesCategoryId { get; set; }

    public int CompaniesId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Company Companies { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
