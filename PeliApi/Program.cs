using PeliApi.Data;
using PeliApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<HighScoreService>();

// Change from SQLite to SQL Server
builder.Services.AddDbContext<PongDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// The following lines are automatically included when creating a new ASP.NET Core project.
// Ensure you have them to configure logging:
builder.Logging.AddConsole(); // <-- This will allow logging messages to be displayed in the console.

var app = builder.Build();
app.UseWebSockets();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
