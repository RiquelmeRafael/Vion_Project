using Vion.Web.Models.Dtos;

namespace Vion.Web.Models;

public class CatalogViewModel
{
    public IEnumerable<ProdutoDto> Produtos { get; set; }
        = Enumerable.Empty<ProdutoDto>();

    public IEnumerable<CategoriaDto> Categorias { get; set; }
        = Enumerable.Empty<CategoriaDto>();

    public IEnumerable<TamanhoDto> Tamanhos { get; set; }
        = Enumerable.Empty<TamanhoDto>();

    // filtros selecionados
    public int? CategoriaSelecionadaId { get; set; }
    public int? TamanhoSelecionadoId { get; set; }
    public string? Busca { get; set; }
    public string? Ordem { get; set; }
}
