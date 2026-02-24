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
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<ChatConversation> ChatConversations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<HomeHero> HomeHeros { get; set; }
        public DbSet<HomeFeaturedItem> HomeFeaturedItems { get; set; }

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

            // =======================
            // FAVORITOS
            // =======================
            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Usuario)
                .WithMany()
                .HasForeignKey(f => f.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Produto)
                .WithMany()
                .HasForeignKey(f => f.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(c => c.Produto)
                .WithMany()
                .HasForeignKey(c => c.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatConversation>()
                .HasOne(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatConversation>()
                .HasOne(c => c.Atendente)
                .WithMany()
                .HasForeignKey(c => c.AtendenteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Mensagens)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Remetente)
                .WithMany()
                .HasForeignKey(m => m.RemetenteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
