using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Vion.Application.Abstractions.Repositories;
using Vion.Infrastructure.Persistence;
using Vion.Infrastructure.Persistence.Seeds;
using Vion.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =======================
// BANCO DE DADOS
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// =======================
// DEPENDÊNCIAS
// =======================
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITamanhoRepository, TamanhoRepository>();

// =======================
// CONTROLLERS + JSON (ANTI LOOP)
// =======================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// =======================
// CORS
// =======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// =======================
// SWAGGER
// =======================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =======================
// SEED AUTOMÁTICO
// =======================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(context);
}

// =======================
// PIPELINE
// =======================
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
