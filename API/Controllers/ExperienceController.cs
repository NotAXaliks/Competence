using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperienceController(CompetenceContext context) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var edu = await context.Experiences.FindAsync(id);
        if (edu == null) return NotFound(new ApiResponse(null, "Не найден", 404));

        return Ok(new ApiResponse(edu));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Experience edu)
    {
        context.Experiences.Add(edu);
        await context.SaveChangesAsync();
        return Ok(new ApiResponse(edu));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Experience edu)
    {
        edu.Id = id;

        context.Entry(edu).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(new ApiResponse(edu));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var edu = await context.Experiences.FindAsync(id);
        if (edu == null) return NotFound(new ApiResponse(null, "Не найден", 404));

        context.Experiences.Remove(edu);
        await context.SaveChangesAsync();

        return Ok(new ApiResponse(true));
    }
}
