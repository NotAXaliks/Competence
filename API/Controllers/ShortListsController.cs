using System.Security.Claims;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ShortlistsController(CompetenceContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMyShortLists()
    {
        var lists = await context.ShortLists
            .Where(s => s.Hrid.ToString() == User.FindFirstValue(ClaimTypes.Name))
            .Include(s => s.Users)
            .ThenInclude(c => c.UserRating)
            .ToListAsync();
        return Ok(new ApiResponse(lists));
    }
}
