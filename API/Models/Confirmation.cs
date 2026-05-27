using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Confirmation
{
    public int Id { get; set; }

    public int RequestorId { get; set; }

    public int TargetId { get; set; }

    public int SkillId { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User Requestor { get; set; } = null!;

    public virtual UserSkill Skill { get; set; } = null!;

    public virtual ConfirmationStatus Status { get; set; } = null!;

    public virtual User Target { get; set; } = null!;
}
