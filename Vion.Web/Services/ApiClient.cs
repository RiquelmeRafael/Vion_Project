using System.Net.Http.Json;
using Vion.Web.Models.Dtos;

namespace Vion.Web.Services;

public class ApiClient : IApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<ProdutoDto>> GetProdutosAsync(
        int? categoriaId = null,
        int? tamanhoId = null,
        string? busca = null,
        string? ordem = null)
    {
        var url = "/api/Produtos?";

        if (categoriaId.HasValue)
            url += $"categoriaId={categoriaId}&";

        if (tamanhoId.HasValue)
            url += $"tamanhoId={tamanhoId}&";

        if (!string.IsNullOrWhiteSpace(busca))
            url += $"busca={Uri.EscapeDataString(busca)}&";

        if (!string.IsNullOrWhiteSpace(ordem))
            url += $"ordem={ordem}&";

        return await _http.GetFromJsonAsync<IEnumerable<ProdutoDto>>(url)
               ?? Enumerable.Empty<ProdutoDto>();
    }

    public async Task<IEnumerable<CategoriaDto>> GetCategoriasAsync()
        => await _http.GetFromJsonAsync<IEnumerable<CategoriaDto>>("/api/Categorias")
           ?? Enumerable.Empty<CategoriaDto>();

    public async Task<IEnumerable<TamanhoDto>> GetTamanhosAsync()
        => await _http.GetFromJsonAsync<IEnumerable<TamanhoDto>>("/api/Tamanhos")
           ?? Enumerable.Empty<TamanhoDto>();
}
