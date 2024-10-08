using System;
using System.Collections.Generic;

namespace Projetos_App1.Models;

public partial class ShippingMethod
{
    public int ShippingMethodsId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
