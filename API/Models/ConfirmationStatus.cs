using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ConfirmationStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Confirmation> Confirmations { get; set; } = new List<Confirmation>();
}
