using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserRating
{
    public int UserId { get; set; }

    public decimal CompetenceIndex { get; set; }

    public decimal CommunityTrust { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual User User { get; set; } = null!;
}
