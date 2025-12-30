using Microsoft.EntityFrameworkCore;
using Vion.Application.Abstractions.Repositories;
using Vion.Infrastructure.Persistence;
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
// DEPENDÊNCIAS (REPOSITÓRIOS)
// =======================
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITamanhoRepository, TamanhoRepository>();

// =======================
// CONTROLLERS
// =======================
builder.Services.AddControllers();

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
