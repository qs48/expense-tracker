using Microsoft.EntityFrameworkCore;
using UserService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Secret key for JWT (for demo, keep it in appsettings or environment variable)
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    // For demo purposes only — in production, use environment variables
    jwtKey = "ThisIsASuperSecretKeyWithAtLeast32Chars!";
}
// Register DbContext with MariaDB
builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(11, 3, 2)) // adjust to your MariaDB version
    )
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Add controllers (for future UsersController)
builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Simple health check
app.MapGet("/", () => "UserService running!");

app.MapControllers();

// Endpoint to list all users
app.MapGet("/users", async (UserContext db) =>
{
    var users = await db.Users.ToListAsync();
    return Results.Ok(users);
});

app.Run();
