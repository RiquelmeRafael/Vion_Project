using Microsoft.AspNetCore.Mvc;
using Vion.Web.Models;
using Vion.Web.Services;

namespace Vion.Web.Controllers;

public class HomeController : Controller
{
    private readonly IApiClient _api;

    public HomeController(IApiClient api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(int? categoriaId, int? tamanhoId, string? busca, string? ordem)
    {
        var viewModel = new CatalogViewModel
        {
            Produtos = await _api.GetProdutosAsync(categoriaId, tamanhoId, busca, ordem),
            Categorias = await _api.GetCategoriasAsync(),
            Tamanhos = await _api.GetTamanhosAsync(),
            CategoriaSelecionadaId = categoriaId,
            TamanhoSelecionadoId = tamanhoId,
            Busca = busca,
            Ordem = ordem
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Catalog(int? categoriaId, int? tamanhoId, string? busca, string? ordem)
    {
        var viewModel = new CatalogViewModel
        {
            Produtos = await _api.GetProdutosAsync(categoriaId, tamanhoId, busca, ordem),
            Categorias = await _api.GetCategoriasAsync(),
            Tamanhos = await _api.GetTamanhosAsync(),
            CategoriaSelecionadaId = categoriaId,
            TamanhoSelecionadoId = tamanhoId,
            Busca = busca,
            Ordem = ordem
        };

        return View(viewModel);
    }

    public IActionResult About()
    {
        return View();
    }
}
