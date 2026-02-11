namespace Vion.Application.DTOs;

public class ProdutoUpdateDto
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
