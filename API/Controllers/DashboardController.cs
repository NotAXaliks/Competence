using System.Security.Claims;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public record DashboardSummary(string ActiveProfiles, string JobMatch, string AvgRating);

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController(CompetenceContext context) : ControllerBase
{
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var totalUsers = await context.Users.CountAsync();
        var yesterday = DateTimeOffset.UtcNow.AddDays(-1);
        var growth = await context.Users.CountAsync(u => u.CreatedAt > yesterday);

        var avgRating = await context.UserRatings.AverageAsync(r => (double?)r.CompetenceIndex) ?? 0;

        return Ok(new ApiResponse(new DashboardSummary($"{totalUsers} (+{growth})", "68% (-2%)", $"{avgRating:F1}/10 (+0.3)")));
    }
}