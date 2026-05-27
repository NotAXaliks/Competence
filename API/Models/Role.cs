using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RoleHistory> RoleHistoryNewRoles { get; set; } = new List<RoleHistory>();

    public virtual ICollection<RoleHistory> RoleHistoryOldRoles { get; set; } = new List<RoleHistory>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
