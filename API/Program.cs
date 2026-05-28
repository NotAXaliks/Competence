using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            []
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<CompetenceContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Allow");
app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var sw = Stopwatch.StartNew();
    await next();

    var db = context.RequestServices.GetRequiredService<CompetenceContext>();
    var userIdStr = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    db.ApiLogs.Add(new ApiLog
    {
        UserId = int.TryParse(userIdStr, out int id) ? id : null,
        Method = context.Request.Method,
        Endpoint = context.Request.Path,
        StatusCode = context.Response.StatusCode,
        DurationMs = (int)sw.ElapsedMilliseconds,
    });
    
    await db.SaveChangesAsync();
});

app.MapControllers();

app.Run();
