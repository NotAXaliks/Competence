using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ShortListCandidate
{
    public int Id { get; set; }

    public int ShortListId { get; set; }

    public int UserId { get; set; }

    public virtual ShortList ShortList { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
