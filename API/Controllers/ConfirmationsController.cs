using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public record ConfirmRequest(int TargetId, int SkillId);

[Route("api/[controller]")]
[ApiController]
public class ConfirmationsController(CompetenceContext context) : ControllerBase
{
    [HttpPost("request")]
    public async Task<IActionResult> Request([FromBody] ConfirmRequest req)
    {
        var myId = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
        
        if (myId == req.TargetId) 
            return BadRequest(new ApiResponse(default, "Нельзя подтверждать самого себя", 400));

        var confirm = new Confirmation {
            RequestorId = myId,
            TargetId = req.TargetId,
            SkillId = req.SkillId,
            StatusId = 3, // "pending"
        };

        context.Confirmations.Add(confirm);
        await context.SaveChangesAsync();

        return Ok(new ApiResponse(req));
    }

    [HttpPost("{id:int}/accept")]
    public async Task<IActionResult> Accept(int id)
    {
        var confirm = await context.Confirmations.FindAsync(id);
        if (confirm == null) return NotFound(new ApiResponse(null, "Не найден", 404));

        if (confirm.StatusId != 3) return BadRequest(new ApiResponse(null, "Подтверждение уже принято", 400));

        confirm.StatusId = 4;
        confirm.UpdatedAt = DateTime.UtcNow;

        var userSkill = await context.UserSkills.FindAsync(confirm.SkillId);
        userSkill.ConfirmationsCount++;

        await context.SaveChangesAsync();

        return Ok(new ApiResponse(confirm));
    }

    [HttpPost("{id:int}/reject")]
    public async Task<IActionResult> Reject(int id)
    {
        var confirm = await context.Confirmations.FindAsync(id);
        if (confirm == null) return NotFound(new ApiResponse(null, "Не найден", 404));

        if (confirm.StatusId != 3) return BadRequest(new ApiResponse(null, "Подтверждение уже принято", 400));

        confirm.StatusId = 5;
        confirm.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return Ok(new ApiResponse(confirm));
    }

    [HttpGet]
    public async Task<IActionResult> GetConfirmations([FromQuery] int? status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
        var query = context.Confirmations.Where(c => c.RequestorId == userId || c.TargetId == userId);

        if (status != null) query = query.Where(c => c.StatusId == status);
        
        var data = await query.Include(c => c.Skill).ToListAsync();

        return Ok(new ApiResponse(data));
    }
}