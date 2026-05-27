using System;
using System.Collections.Generic;

namespace API.Models;

public partial class RatingHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Value { get; set; }

    public DateTime Date { get; set; }

    public virtual User User { get; set; } = null!;
}
