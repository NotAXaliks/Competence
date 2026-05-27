using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

public record LoginResponse(User User, string Token);
public record LoginRequest(string Email, string Password);

[Route("api/[controller]")]
[ApiController]
public class AuthController(CompetenceContext context, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
        if (user == null) return Unauthorized(new ApiResponse(null, "Неверный логин или пароль", 403));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: creds
        );

        return Ok(new ApiResponse(new LoginResponse(user, new JwtSecurityTokenHandler().WriteToken(token))));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(string token)
    {
        return Ok(new ApiResponse(default, "Not implemented", 500));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        return Ok(new ApiResponse(default, "Not implemented", 500));
    }
}