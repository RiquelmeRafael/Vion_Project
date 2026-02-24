using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vion.Domain.Entities
{
    public class Cupom
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Codigo { get; set; } = null!;

        [Column(TypeName = "decimal(5,2)")]
        public decimal PercentualDesconto { get; set; } // Ex: 10.00 para 10%

        public bool Ativo { get; set; } = true;

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
