using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserSkill
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SkillId { get; set; }

    public int Level { get; set; }

    public int ConfirmationsCount { get; set; }

    public virtual ICollection<Confirmation> Confirmations { get; set; } = new List<Confirmation>();

    public virtual Skill Skill { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
