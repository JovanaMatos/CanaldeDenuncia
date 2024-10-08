using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class CompanyRelation
{
    public int CompanyRelationId { get; set; }

    public string CompanyRelationship { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
