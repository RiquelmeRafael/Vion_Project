using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;

namespace Vion.Infrastructure.Persistence
{
    public class AppDbContext
        : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // =======================
        // TABELAS DO DOMÍNIO
        // =======================
        public DbSet<TipoUsuario> TiposUsuario { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ⚠️ OBRIGATÓRIO para Identity
            base.OnModelCreating(modelBuilder);

            // =======================
            // PRODUTO
            // =======================
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Tamanho)
                .WithMany(t => t.Produtos)
                .HasForeignKey(p => p.TamanhoId)
                .OnDelete(DeleteBehavior.Restrict);

            // =======================
            // USUARIO → TIPOUSUARIO
            // =======================
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.TipoUsuario)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(u => u.TipoUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
