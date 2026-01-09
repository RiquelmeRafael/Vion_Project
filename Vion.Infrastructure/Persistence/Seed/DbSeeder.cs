using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vion.Domain.Entities;

namespace Vion.Infrastructure.Persistence.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            IServiceProvider services)
        {
            // =======================
            // EVITA RODAR DUAS VEZES
            // =======================
            if (await context.TiposUsuario.AnyAsync())
                return;

            // =======================
            // TIPOS DE USUÁRIO (PRIMEIRO DE TUDO)
            // =======================
            var tipoAdmin = new TipoUsuario { Nome = "Admin" };
            var tipoGerente = new TipoUsuario { Nome = "Gerente" };
            var tipoCliente = new TipoUsuario { Nome = "Cliente" };

            context.TiposUsuario.AddRange(
                tipoAdmin,
                tipoGerente,
                tipoCliente
            );

            await context.SaveChangesAsync();

            // =======================
            // IDENTITY SERVICES
            // =======================
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole<int>>>();

            var userManager =
                services.GetRequiredService<UserManager<Usuario>>();

            // =======================
            // ROLES
            // =======================
            var roles = new[] { "Admin", "Gerente", "Cliente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole<int>(role)
                    );
                }
            }

            // =======================
            // USUÁRIOS (COM FK CORRETA)
            // =======================
            async Task CriarUsuario(
                string nome,
                string email,
                string role,
                int tipoUsuarioId)
            {
                if (await userManager.FindByEmailAsync(email) != null)
                    return;

                var user = new Usuario
                {
                    Nome = nome,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    TipoUsuarioId = tipoUsuarioId
                };

                var result = await userManager.CreateAsync(
                    user,
                    $"{role}@123"
                );

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            await CriarUsuario(
                "Administrador",
                "admin@vion.com",
                "Admin",
                tipoAdmin.Id
            );

            await CriarUsuario(
                "Gerente",
                "gerente@vion.com",
                "Gerente",
                tipoGerente.Id
            );

            await CriarUsuario(
                "Cliente",
                "cliente@vion.com",
                "Cliente",
                tipoCliente.Id
            );

            // =======================
            // TAMANHOS
            // =======================
            var tamanhoP = new Tamanho { Nome = "P" };
            var tamanhoM = new Tamanho { Nome = "M" };
            var tamanhoG = new Tamanho { Nome = "G" };
            var tamanhoGG = new Tamanho { Nome = "GG" };
            var tamanho36 = new Tamanho { Nome = "36" };
            var tamanho38 = new Tamanho { Nome = "38" };
            var tamanho40 = new Tamanho { Nome = "40" };

            context.Tamanhos.AddRange(
                tamanhoP,
                tamanhoM,
                tamanhoG,
                tamanhoGG,
                tamanho36,
                tamanho38,
                tamanho40
            );

            await context.SaveChangesAsync();

            // =======================
            // CATEGORIAS (SEM LINQ)
            // =======================
            var categoriaCamisas = new Categoria { Nome = "Camisas" };
            var categoriaTenis = new Categoria { Nome = "Tênis" };
            var categoriaMoletons = new Categoria { Nome = "Moletons" };

            context.Categorias.AddRange(
                categoriaCamisas,
                categoriaTenis,
                categoriaMoletons
            );

            await context.SaveChangesAsync();

            // =======================
            // PRODUTO
            // =======================
            var produto = new Produto
            {
                Nome = "Camiseta Básica",
                Descricao = "100% algodão",
                Preco = 59.90m,
                CategoriaId = categoriaCamisas.Id,
                TamanhoId = tamanhoM.Id,
                Cor = "Branca",
                Estoque = 50,
                ImagemUrl = "https://via.placeholder.com/300",
                CreatedAt = DateTime.UtcNow
            };

            context.Produtos.Add(produto);
            await context.SaveChangesAsync();
        }
    }
}
