using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Infrastructure.Persistence.Seeds;
using Vion.Web.Services;


var builder = WebApplication.CreateBuilder(args);

// =======================
// MVC
// =======================
builder.Services.AddControllersWithViews();

// =======================
// DATABASE
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// =======================
// IDENTITY
// =======================
builder.Services
    .AddIdentity<Usuario, IdentityRole<int>>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

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
// API CLIENT
// =======================
builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
    client.BaseAddress = apiBaseUri;
});

var app = builder.Build();

// =======================
// SEED AUTOMÁTICO
// =======================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    await DbSeeder.SeedAsync(context, services);
}

// =======================
// PIPELINE
// =======================
app.UseHttpsRedirection();
app.UseStaticFiles();

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