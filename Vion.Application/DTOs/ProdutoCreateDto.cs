namespace Vion.Application.DTOs;

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
}
