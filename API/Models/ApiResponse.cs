using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models;

public record ApiResponse(object? Data, string? Error = null, int? ErrorCode = null)
{
    public bool Success => Error == null;
}

public record ApiResponse<T>(T Data, string? Error = null, int? ErrorCode = null)
{
    public bool Success => Error == null;
}
