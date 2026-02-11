namespace Vion_Desktop.Models
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
        public string Categoria { get; set; } = string.Empty; // Nome da Categoria
        public int TamanhoId { get; set; }
        public string Tamanho { get; set; } = string.Empty;   // Nome do Tamanho
        public string Cor { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public string? ImagemUrl { get; set; }
        public string? ImagemUrl2 { get; set; }
        public string? ImagemUrl3 { get; set; }
        public string? ImagemUrl4 { get; set; }
        public int? CupomId { get; set; }
    }

    public class ProdutoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
        public int TamanhoId { get; set; }
        public string Cor { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public string? ImagemUrl { get; set; }
        public string? ImagemUrl2 { get; set; }
        public string? ImagemUrl3 { get; set; }
        public string? ImagemUrl4 { get; set; }
        public int? CupomId { get; set; }
    }

    public class ProdutoUpdateDto : ProdutoCreateDto
    {
        // Mesma estrutura do Create por enquanto
    }
}
