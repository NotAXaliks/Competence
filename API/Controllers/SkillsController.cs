using System.Security.Claims;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController(CompetenceContext context) : ControllerBase
{
    [HttpPost("{userId:int}")]
    public async Task<IActionResult> AddSkill(int userId, [FromBody] UserSkill skill)
    {
        skill.UserId = userId;

        if (skill.Level < 0 || skill.Level > 10) return BadRequest(new ApiResponse(null, "Level 0-10", 400));

        context.UserSkills.Add(skill);
        await context.SaveChangesAsync();
        return Ok(new ApiResponse(skill));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSkillFull(int id, [FromBody] UserSkill skill)
    {
        skill.Id = id;

        context.Entry(skill).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.UserSkills.Any(e => e.Id == id)) return NotFound(new ApiResponse(null, "Не найден", 404));
            
            return NotFound(new ApiResponse(null, "Error", 500));
        }

        return Ok(new ApiResponse(skill));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name)!);
        var skill = await context.UserSkills.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (skill == null) return NotFound();

        context.UserSkills.Remove(skill);
        await context.SaveChangesAsync();
        return Ok(new ApiResponse(true));
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopSkills()
    {
        var totalUsers = await context.Users.CountAsync();
        if (totalUsers == 0) return Ok(new ApiResponse(new {}));

        var topSkills = await context.UserSkills
            .GroupBy(us => us.Skill.Name)
            .Select(g => new 
            { 
                name = g.Key, 
                percent = (int)(g.Count() * 100.0 / totalUsers)
            })
            .OrderByDescending(s => s.percent)
            .Take(5)
            .ToListAsync();

        return Ok(new ApiResponse(topSkills));
    }

    [HttpGet("suggest")]
    public async Task<IActionResult> GetSuggestions([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q)) return Ok(new ApiResponse(await context.Skills.Select(s => s.Name).ToListAsync()));

        var skills = await context.Skills.Where(s => s.Name.ToLower().Contains(q.ToLower())).Select(s => s.Name).ToListAsync();
        return Ok(new ApiResponse(skills));
    }
}