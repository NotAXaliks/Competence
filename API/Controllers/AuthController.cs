using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string FirstName, string MiddleName, string LastName);

[Route("api/[controller]")]
[ApiController]
public class AuthController(CompetenceContext context) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
        if (user == null) return Unauthorized(new ApiResponse(null, "Неверный логин или пароль", 403));

        return Ok(new ApiResponse(user));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
        };

        return StatusCode(201, new ApiResponse(user));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        return StatusCode(501, new ApiResponse(default, "В разработке", 501));
    }
}