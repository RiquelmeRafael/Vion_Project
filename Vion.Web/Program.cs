using Microsoft.EntityFrameworkCore;
using Vion.Infrastructure.Persistence;
using Vion.Infrastructure.Persistence.Seeds;

var builder = WebApplication.CreateBuilder(args);

// =======================
// BANCO DE DADOS
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// =======================
// SEED (SÓ POPULA)
// =======================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(context);
}

// =======================
// PIPELINE
// =======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
