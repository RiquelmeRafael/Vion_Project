﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using Microsoft.AspNetCore.Identity;
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
            // 1. TIPOS DE USUÁRIO (Check before add)
            // =======================
            if (!await context.TiposUsuario.AnyAsync())
            {
                var tipoAdmin = new TipoUsuario { Nome = "Admin" };
                var tipoGerente = new TipoUsuario { Nome = "Gerente" };
                var tipoCliente = new TipoUsuario { Nome = "Cliente" };

                context.TiposUsuario.AddRange(tipoAdmin, tipoGerente, tipoCliente);
                await context.SaveChangesAsync();
            }

            // Recupera referências do banco para garantir que temos os IDs
            var tiposRef = await context.TiposUsuario.ToListAsync();
            var tipoAdminRef = tiposRef.First(t => t.Nome == "Admin");
            var tipoGerenteRef = tiposRef.First(t => t.Nome == "Gerente");
            var tipoClienteRef = tiposRef.First(t => t.Nome == "Cliente");

            // =======================
            // 2. IDENTITY ROLES & USERS
            // =======================
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = services.GetRequiredService<UserManager<Usuario>>();

            var roles = new[] { "Admin", "Gerente", "Cliente" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            async Task CriarUsuario(string nome, string email, string role, int tipoUsuarioId)
            {
                if (await userManager.FindByEmailAsync(email) != null) return;

                var user = new Usuario
                {
                    Nome = nome,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    TipoUsuarioId = tipoUsuarioId
                };

                var result = await userManager.CreateAsync(user, $"{role}@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            await CriarUsuario("Administrador", "admin@vion.com", "Admin", tipoAdminRef.Id);
            await CriarUsuario("Gerente", "gerente@vion.com", "Gerente", tipoGerenteRef.Id);
            await CriarUsuario("Cliente", "cliente@vion.com", "Cliente", tipoClienteRef.Id);

            // =======================
            // 3. TAMANHOS (Check before add)
            // =======================
            if (!await context.Tamanhos.AnyAsync())
            {
                var tamanhos = new List<Tamanho>
                {
                    new Tamanho { Nome = "P" },
                    new Tamanho { Nome = "M" },
                    new Tamanho { Nome = "G" },
                    new Tamanho { Nome = "GG" },
                    new Tamanho { Nome = "36" },
                    new Tamanho { Nome = "38" },
                    new Tamanho { Nome = "40" },
                    new Tamanho { Nome = "Unico" }
                };
                context.Tamanhos.AddRange(tamanhos);
                await context.SaveChangesAsync();
            }

            var tamanhosRef = await context.Tamanhos.ToListAsync();
            // Pega referência do tamanho M para usar como padrão nos produtos
            var tamanhoPadrao = tamanhosRef.First(t => t.Nome == "M");

            // =======================
            // 4. CATEGORIAS (Check before add)
            // =======================
            if (!await context.Categorias.AnyAsync())
            {
                var catTenis = new Categoria { Nome = "Tênis" };
                var catCamiseta = new Categoria { Nome = "Camiseta" };
                var catShort = new Categoria { Nome = "Short" };
                var catCalca = new Categoria { Nome = "Calça" };
                var catAcessorios = new Categoria { Nome = "Acessórios" };

                var categorias = new List<Categoria> { catTenis, catCamiseta, catShort, catCalca, catAcessorios };
                context.Categorias.AddRange(categorias);
                await context.SaveChangesAsync();
            }
            
            var categoriasRef = await context.Categorias.ToListAsync();

            // =======================
            // 5. PRODUTOS (2 POR CATEGORIA)
            // =======================
            if (!await context.Produtos.AnyAsync())
            {
                var produtos = new List<Produto>();
                var random = new Random();
    
                foreach (var categoria in categoriasRef)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        // Preço aleatório entre 50 e 400
                        decimal preco = random.Next(50, 400) + 0.90m;
                        
                        // Imagens de placeholder (cores diferentes para variar)
                        string colorHex = random.Next(0, 16777215).ToString("X");
                        string imgUrl = $"https://via.placeholder.com/320/{colorHex}/FFFFFF?text={categoria.Nome}+{i}";
    
                        // Define tamanho
                        int tamanhoId = tamanhoPadrao.Id;
                        if (categoria.Nome == "Acessórios")
                        {
                            var unico = tamanhosRef.FirstOrDefault(t => t.Nome == "Unico");
                            if (unico != null) tamanhoId = unico.Id;
                        }
    
                        var produto = new Produto
                        {
                            Nome = $"{categoria.Nome} Modelo {i:00}", // Ex: Tênis Modelo 01
                            Descricao = $"Esta é uma descrição detalhada para o {categoria.Nome} modelo {i}. Produto de alta qualidade, confortável e estiloso para o seu dia a dia.",
                            Preco = preco,
                            CategoriaId = categoria.Id,
                            TamanhoId = tamanhoId,
                            Cor = "Variada",
                            Estoque = random.Next(10, 100),
                            ImagemUrl = imgUrl,
                            ImagemUrl2 = imgUrl, // Repete para testar o grid de fotos
                            ImagemUrl3 = imgUrl,
                            ImagemUrl4 = imgUrl,
                            CreatedAt = DateTime.UtcNow
                        };
    
                        produtos.Add(produto);
                    }
                }
    
                // Otimização: Salvar em lotes menores para evitar Timeout
                // Mesmo com poucos produtos, isso é uma boa prática
                var batchSize = 50;
                for (int i = 0; i < produtos.Count; i += batchSize)
                {
                    var batch = produtos.Skip(i).Take(batchSize).ToList();
                    context.Produtos.AddRange(batch);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
