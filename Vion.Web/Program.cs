using Vion.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// =======================
// MVC
// =======================
builder.Services.AddControllersWithViews();

// =======================
// HTTP CLIENT (API)
// =======================
builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];

    if (string.IsNullOrEmpty(baseUrl))
        throw new InvalidOperationException("ApiSettings:BaseUrl não configurado");

    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// =======================
// PIPELINE
// =======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // ✅ IMPORTANTE
app.UseStaticFiles();
app.UseRouting();

// =======================
// ROTAS MVC
// =======================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// evita 404 estranho quando rota existe
app.MapFallbackToController("Index", "Home");

app.Run();
