using Microsoft.EntityFrameworkCore;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with MariaDB
builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(11, 3, 2)) // adjust to your MariaDB version
    )
);

// Add controllers (for future UsersController)
builder.Services.AddControllers();

var app = builder.Build();

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
