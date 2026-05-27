using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Experience
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CompanyId { get; set; }

    public string EmploymentType { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Organization Company { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
