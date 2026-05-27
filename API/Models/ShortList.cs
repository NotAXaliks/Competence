using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ShortList
{
    public int Id { get; set; }

    public int Hrid { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User Hr { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
