using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vion.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorDesconto { get; set; }

        public int? CupomId { get; set; }
        public Cupom? Cupom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pendente"; // Pendente, Aprovado, Cancelado

        [Required]
        [MaxLength(50)]
        public string FormaPagamento { get; set; } = null!; // Pix, CartaoCredito
        public decimal ValorFrete { get; set; }

        // Dados do Cliente no momento da compra
        [MaxLength(14)]
        public string? Cpf { get; set; }

        [MaxLength(100)]
        public string? NomeCliente { get; set; }

        [MaxLength(100)]
        public string? EmailCliente { get; set; }

        // Endereço de Entrega
        [MaxLength(9)]
        public string? Cep { get; set; }

        [MaxLength(200)]
        public string? Endereco { get; set; }

        [MaxLength(100)]
        public string? Cidade { get; set; }

        [MaxLength(2)]
        public string? Estado { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
