namespace Vion.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public int TamanhoId { get; set; }      // 🔥 FK
        public Tamanho Tamanho { get; set; } = null!;    // 🔥 Navegação

        public string Cor { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public decimal? ValorFreteFixo { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public string? ImagemUrl2 { get; set; }
        public string? ImagemUrl3 { get; set; }
        public string? ImagemUrl4 { get; set; }
        
        public int? CupomId { get; set; }
        public Cupom? Cupom { get; set; }

        public DateTime CreatedAt { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}
