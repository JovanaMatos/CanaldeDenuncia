using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class Responsible
{
    public int ResponsiblesId { get; set; }

    public string ResponsibleName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<ResponsibleHistory> ResposibleHistories { get; set; } = new List<ResponsibleHistory>();
}
