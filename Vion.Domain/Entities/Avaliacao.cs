using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vion.Domain.Entities
{
    public class Avaliacao
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        
        // Opcional: vincula ao pedido específico para garantir compra verificada
        public int? PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        [Range(0, 10)]
        public int Nota { get; set; }

        [MaxLength(1000)]
        public string? Comentario { get; set; }

        public string? ImagemUrl { get; set; }

        public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;
    }
}
