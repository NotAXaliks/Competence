using System;
using System.Collections.Generic;

namespace API.Models;

public partial class RoleHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OldRoleId { get; set; }

    public int NewRoleId { get; set; }

    public DateTime Date { get; set; }

    public virtual Role NewRole { get; set; } = null!;

    public virtual Role OldRole { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
