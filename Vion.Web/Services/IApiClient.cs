using Vion.Web.Models.Dtos;

namespace Vion.Web.Services;

public interface IApiClient
{
    Task<IEnumerable<ProdutoDto>> GetProdutosAsync(
        int? categoriaId = null,
        int? tamanhoId = null,
        string? busca = null,
        string? ordem = null
    );

    Task<ProdutoDto?> GetProdutoByIdAsync(int id);

    Task<IEnumerable<CategoriaDto>> GetCategoriasAsync();
    Task<IEnumerable<TamanhoDto>> GetTamanhosAsync();
}
