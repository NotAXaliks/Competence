using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Education
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int InstitutionId { get; set; }

    public int EducationTypeId { get; set; }

    public int Order { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual EducationType EducationType { get; set; } = null!;

    public virtual Institution Institution { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
