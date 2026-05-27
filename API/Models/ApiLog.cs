using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ApiLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Method { get; set; } = null!;

    public string Endpoint { get; set; } = null!;

    public int StatusCode { get; set; }

    public int DurationMs { get; set; }

    public DateTime Date { get; set; }

    public virtual User? User { get; set; }
}
