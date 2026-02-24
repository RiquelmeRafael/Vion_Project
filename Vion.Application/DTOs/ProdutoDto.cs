namespace Vion.Application.DTOs;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }

    public int CategoriaId { get; set; }
    public string Categoria { get; set; } = string.Empty;

    public int TamanhoId { get; set; }
    public string Tamanho { get; set; } = string.Empty;

    public string? Cor { get; set; }
    public int Estoque { get; set; }
    public decimal? ValorFreteFixo { get; set; }
    public string? ImagemUrl { get; set; }
    public string? ImagemUrl2 { get; set; }
    public string? ImagemUrl3 { get; set; }
    public string? ImagemUrl4 { get; set; }

    public int? CupomId { get; set; }
    public string? CupomCodigo { get; set; }
}
