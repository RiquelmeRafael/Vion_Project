using Microsoft.EntityFrameworkCore;
using Vion.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// registra o banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => new { name = "Vion.Api", status = "ok" });

app.MapControllers();

app.Run();
