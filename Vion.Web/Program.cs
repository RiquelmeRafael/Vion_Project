using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Infrastructure.Persistence.Seeds;
using Vion.Web.Services;
using Vion.Application.Abstractions.Repositories;
using Vion.Infrastructure.Repositories;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Parse("10.136.46.31"), 5200);
});

// =======================
// MVC
// =======================
builder.Services.AddControllersWithViews();

// =======================
// SESSION
// =======================
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// =======================
// DATABASE
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
            .CommandTimeout(300) // Aumenta timeout para 5 minutos
    )
);

// =======================
// IDENTITY
// =======================
builder.Services
    .AddIdentity<Usuario, IdentityRole<int>>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

// =======================
// POLICIES
// =======================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",
        policy => policy.RequireRole("Admin"));

    options.AddPolicy("ManagerOrAdmin",
        policy => policy.RequireRole("Admin", "Gerente"));
});

// =======================
// API SETTINGS (bind + validação)
// =======================
var apiBaseFromLegacyKey = builder.Configuration["ApiBaseUrl"];
var apiSettingsSection = builder.Configuration.GetSection("ApiSettings");
var apiSettings = apiSettingsSection.Exists()
    ? apiSettingsSection.Get<ApiSettings>()
    : null;

// Permitir chave antiga ("ApiBaseUrl") como fallback
var apiBaseRaw = string.IsNullOrWhiteSpace(apiBaseFromLegacyKey)
    ? apiSettings?.BaseUrl
    : apiBaseFromLegacyKey;

if (string.IsNullOrWhiteSpace(apiBaseRaw))
{
    throw new InvalidOperationException(
        "A configuração da API não foi encontrada. Defina 'ApiSettings:BaseUrl' (ou 'ApiBaseUrl' para compatibilidade).");
}

if (!Uri.TryCreate(apiBaseRaw, UriKind.Absolute, out var apiBaseUri))
{
    throw new InvalidOperationException(
        "A configuração 'ApiSettings:BaseUrl' contém um valor inválido. Informe uma URL absoluta válida.");
}

// Registra ApiSettings para injeção se necessário em outros pontos
builder.Services.Configure<ApiSettings>(apiSettingsSection);

// =======================
// SERVICES
// =======================
builder.Services.AddScoped<Vion.Web.Areas.Admin.Services.DashboardService>();
builder.Services.AddHttpClient<IFreteService, FreteService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// =======================
// REPOSITORIES (Otimização)
// =======================
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITamanhoRepository, TamanhoRepository>();

// =======================
// API CLIENT (Direct)
// =======================
builder.Services.AddScoped<IApiClient, DirectApiClient>();
// builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
// {
//     client.BaseAddress = apiBaseUri;
// });

var app = builder.Build();

// =======================
// SEED AUTOMÁTICO
// =======================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    try
    {
        // Aplica migrações pendentes e cria o banco se não existir
        await context.Database.MigrateAsync();
        
        await DbSeeder.SeedAsync(context, services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados (Migration/Seed). Verifique a ConnectionString e se o SQL Server está rodando.");
        throw;
    }
}

// =======================
// PIPELINE
// =======================
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

// Tipo auxiliar local para bind de configuração
internal sealed record ApiSettings(string? BaseUrl);
