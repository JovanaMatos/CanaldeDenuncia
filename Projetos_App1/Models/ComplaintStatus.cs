using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class ComplaintStatus
{
    public int ComplaintStatusId { get; set; }

    public string CurrentStatus { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
