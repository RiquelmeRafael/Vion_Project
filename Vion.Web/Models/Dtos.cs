namespace Vion.Web.Models.Dtos;

public class ProdutoDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = "";

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public int CategoriaId { get; set; }

    // 🔹 usado pelo Razor (@produto.Categoria)
    public string Categoria { get; set; } = "";

    public int TamanhoId { get; set; }

    // 🔹 usado pelo Razor (@produto.Tamanho)
    public string Tamanho { get; set; } = "";

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

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
}

public class TamanhoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
}
