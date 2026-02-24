using System.ComponentModel.DataAnnotations.Schema;

namespace Vion.Domain.Entities
{
    public class CarrinhoItem
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int ProdutoId { get; set; } // Referência à variante específica (que é um Produto com TamanhoId)
        public Produto Produto { get; set; } = null!;

        public int Quantidade { get; set; }

        public DateTime DataAdicionado { get; set; } = DateTime.UtcNow;
    }
}
