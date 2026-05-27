using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
}
