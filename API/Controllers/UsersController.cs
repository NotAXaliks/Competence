using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public record UserEmployeeData(int Id, string Name, string Avatar, Role Role, UserSkill[] Skills, Experience[] Experiences);

[Route("api/[controller]")]
[ApiController]
public class UsersController(CompetenceContext context) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id, [FromQuery] string? mode)
    {
        var user = await context.Users
            .Include(u => u.UserSkills)
            .ThenInclude(s => s.Skill)
            .Include(u => u.Role)
            .Include(u => u.Experiences)
            .ThenInclude(e => e.Company)
            .Include(e => e.Educations)
            .ThenInclude(e => e.EducationType)
            .Include(e => e.Educations)
            .ThenInclude(e => e.Institution)
            .FirstOrDefaultAsync(u => u.Id.ToString() == id);
        if (user == null) return NotFound(new ApiResponse(null, "Не найден", 404));

        if (mode == "employer")
        {
            return Ok(new ApiResponse(new UserEmployeeData(user.Id, user.Name, user.Avatar, user.Role, user.UserSkills.ToArray(), user.Experiences.ToArray())));
        }

        return Ok(new ApiResponse(user));
    }

    [HttpGet("{id:int}/rating")]
    public async Task<IActionResult> GetUserRating(string id)
    {
        var rating = await context.UserRatings.FirstOrDefaultAsync(r => r.UserId.ToString() == id);

        return Ok(new ApiResponse(rating));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] string? skills = "",
    [FromQuery] int level = 0,
    [FromQuery] int exp = 0,
    [FromQuery] int rating = 0,
    [FromQuery] int limit = 25)
    {
        var skillList = skills.Split(',').Select(s => s.Trim()).ToList();

        var query = context.Users
            .Include(u => u.UserSkills).ThenInclude(us => us.Skill)
            .Include(u => u.Experiences)
            .Include(u => u.UserRating)
            .AsQueryable();

        // if (skillList.Any())
        //     query = query.Where(u => u.UserSkills.Any(us => skillList.Contains(us.Skill.Name)));

        // if (level > 0)
        //     query = query.Where(u => u.UserSkills.Any(us => us.Level >= level));

        // if (rating > 0)
        //     query = query.Where(u => u.UserRating.CompetenceIndex >= rating);

        // if (exp > 0)
        // {
        //     query = query.Where(u => u.Experiences.Sum(e =>
        //         (e.EndDate ?? DateTime.Now).Year - e.StartDate.Year) >= exp);
        // }

        var users = await query.Take(limit).ToListAsync();

        var result = users.Select(u => new
        {
            PublicId = $"#ITP-{u.Id}",
            u.Name,
            Skills = u.UserSkills.Select(us => us.Skill.Name).ToList(),
            Rating = (int)(u.UserRating?.CompetenceIndex ?? 0),
            Trend = 1 // 1 (рост), 0 (без изм), -1 (падение) [2]
        }).ToList();

        return Ok(new ApiResponse(result));
    }
}