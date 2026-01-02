using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Infrastructure.Persistence.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // =======================
            // EVITA DUPLICAR SEED
            // =======================
            if (await context.Produtos.AnyAsync())
                return;

            // =======================
            // TIPOS DE USUÁRIO
            // =======================
            var tiposUsuario = new List<TipoUsuario>
            {
                new() { Nome = "Admin" },
                new() { Nome = "Gerente" },
                new() { Nome = "Cliente" }
            };

            context.TiposUsuario.AddRange(tiposUsuario);
            await context.SaveChangesAsync();

            // =======================
            // USUÁRIOS
            // =======================
            var hasher = new PasswordHasher<Usuario>();

            var admin = new Usuario
            {
                Nome = "Administrador",
                Email = "admin@vion.com",
                TipoUsuarioId = tiposUsuario.First(t => t.Nome == "Admin").Id,
                SenhaHash = hasher.HashPassword(null!, "Admin@123")
            };

            var gerente = new Usuario
            {
                Nome = "Gerente",
                Email = "gerente@vion.com",
                TipoUsuarioId = tiposUsuario.First(t => t.Nome == "Gerente").Id,
                SenhaHash = hasher.HashPassword(null!, "Gerente@123")
            };

            var cliente = new Usuario
            {
                Nome = "Cliente",
                Email = "cliente@vion.com",
                TipoUsuarioId = tiposUsuario.First(t => t.Nome == "Cliente").Id,
                SenhaHash = hasher.HashPassword(null!, "Cliente@123")
            };

            context.Usuarios.AddRange(admin, gerente, cliente);
            await context.SaveChangesAsync();

            // =======================
            // TAMANHOS
            // =======================
            var tamanhos = new List<Tamanho>
            {
                new() { Nome = "P" },
                new() { Nome = "M" },
                new() { Nome = "G" },
                new() { Nome = "GG" },
                new() { Nome = "36" },
                new() { Nome = "38" },
                new() { Nome = "40" }
            };

            context.Tamanhos.AddRange(tamanhos);
            await context.SaveChangesAsync();

            // =======================
            // CATEGORIAS
            // =======================
            var categorias = new List<Categoria>
            {
                new() { Nome = "Camisas" },
                new() { Nome = "Tênis" },
                new() { Nome = "Moletons" }
            };

            context.Categorias.AddRange(categorias);
            await context.SaveChangesAsync();

            // =======================
            // PRODUTOS (CORRETO)
            // =======================
            var produtos = new List<Produto>
            {
                new()
                {
                    Nome = "Camiseta Básica",
                    Descricao = "100% algodão",
                    Preco = 59.90m,
                    CategoriaId = categorias.First(c => c.Nome == "Camisas").Id,
                    TamanhoId = tamanhos.First(t => t.Nome == "M").Id, // ✅ AQUI ESTÁ A CORREÇÃO
                    Cor = "Branca",
                    Estoque = 50,
                    ImagemUrl = "https://via.placeholder.com/300",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Produtos.AddRange(produtos);
            await context.SaveChangesAsync();
        }
    }
}
