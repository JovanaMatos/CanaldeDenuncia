using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class ResposibleHistory
{
    public int ResposibleHistory1 { get; set; }

    public DateTime DataIn { get; set; }

    public DateTime? DataOu { get; set; }

    public Guid ComplaintId { get; set; }

    public int ResponsiblesId { get; set; }

    public virtual Complaint Complaint { get; set; } = null!;

    public virtual Responsible Responsibles { get; set; } = null!;
}
