using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;

namespace Vion.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =======================
            // CONFIGURAÇÕES
            // =======================
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            // =======================
            // SEED - CATEGORIAS
            // =======================
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Camisas" },
                new Categoria { Id = 2, Nome = "Tênis" },
                new Categoria { Id = 3, Nome = "Moletons" }
            );

            // =======================
            // SEED - TAMANHOS
            // =======================
            modelBuilder.Entity<Tamanho>().HasData(
                new Tamanho { Id = 1, Nome = "P" },
                new Tamanho { Id = 2, Nome = "M" },
                new Tamanho { Id = 3, Nome = "G" },
                new Tamanho { Id = 4, Nome = "GG" },
                new Tamanho { Id = 5, Nome = "36" },
                new Tamanho { Id = 6, Nome = "37" },
                new Tamanho { Id = 7, Nome = "38" },
                new Tamanho { Id = 8, Nome = "39" },
                new Tamanho { Id = 9, Nome = "40" },
                new Tamanho { Id = 10, Nome = "41" },
                new Tamanho { Id = 11, Nome = "42" }
            );

            // =======================
            // SEED - TIPOS DE USUÁRIO
            // =======================
            modelBuilder.Entity<TipoUsuario>().HasData(
                new TipoUsuario { Id = 1, Nome = "Admin" },
                new TipoUsuario { Id = 2, Nome = "Gerente" },
                new TipoUsuario { Id = 3, Nome = "Cliente" }
            );
        }
    }
}
