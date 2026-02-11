using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Infrastructure.Persistence;
using Vion.Web.Models;
using Vion.Web.Services;

namespace Vion.Web.Controllers;

public class HomeController : Controller
{
    private readonly IApiClient _api;
    private readonly AppDbContext _context;

    public HomeController(IApiClient api, AppDbContext context)
    {
        _api = api;
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoriaId, int? tamanhoId, string? busca, string? ordem)
    {
        var hero = await _context.HomeHeros
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.IsActive);

        var destaques = await _context.HomeFeaturedItems
            .AsNoTracking()
            .Where(h => h.IsActive)
            .OrderBy(h => h.Position)
            .ToListAsync();

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

        ViewBag.HomeHero = hero;
        ViewBag.HomeFeaturedItems = destaques;

        return View(viewModel);
    }

    // 1. PÁGINA "PRODUTOS" (LISTA DE CATEGORIAS)
    public async Task<IActionResult> Catalog()
    {
        var categorias = await _api.GetCategoriasAsync();
        var cards = new List<Vion.Web.Models.ViewModels.CategoryCardViewModel>();

        foreach (var cat in categorias)
        {
            // =========================================================================================
            // CONFIGURAÇÃO DAS IMAGENS DOS CARDS
            // =========================================================================================
            // Você pode definir manualmente as imagens de cada categoria aqui.
            // Basta colocar o nome da categoria e o link da imagem (pode ser URL ou caminho local).
            
            string? imagem = null;

            if (cat.Nome.Equals("Tênis", StringComparison.OrdinalIgnoreCase))
            {
                // Exemplo 1: Usando uma imagem que está na pasta wwwroot/images/categories/
                // imagem = "/images/categories/tenis_capa.jpg";
                
                // Exemplo 2: Usando uma URL da internet
                // imagem = "https://exemplo.com/foto-tenis.jpg";

                // Se deixar null ou comentar, ele vai tentar pegar a foto do primeiro produto automaticamente.
            }
            else if (cat.Nome.Equals("Roupas", StringComparison.OrdinalIgnoreCase))
            {
                // imagem = "/images/categories/roupas_capa.jpg";
            }

            // =========================================================================================
            // LÓGICA AUTOMÁTICA (Se não definiu imagem acima, pega do primeiro produto)
            // =========================================================================================
            if (string.IsNullOrEmpty(imagem))
            {
                var produtosDaCategoria = await _api.GetProdutosAsync(categoriaId: cat.Id);
                // Pega o produto mais antigo (menor ID) para ilustrar a categoria (Fixa a imagem do primeiro produto cadastrado)
                imagem = produtosDaCategoria.OrderBy(p => p.Id).FirstOrDefault()?.ImagemUrl 
                             ?? "https://via.placeholder.com/320/000000/FFFFFF?text=" + cat.Nome;
            }

            cards.Add(new Vion.Web.Models.ViewModels.CategoryCardViewModel
            {
                Id = cat.Id,
                Nome = cat.Nome,
                ImagemUrl = imagem
            });
        }

        return View("Categories", cards);
    }

    // 2. PÁGINA DE PRODUTOS DE UMA CATEGORIA
    [Route("Produtos/{categoriaId:int}")]
    public async Task<IActionResult> Products(int categoriaId, int? tamanhoId, string? busca, string? ordem)
    {
        var produtos = await _api.GetProdutosAsync(categoriaId, tamanhoId, busca, ordem);
        var categorias = await _api.GetCategoriasAsync(); // Para exibir o nome da categoria no título se precisar

        // Agrupar por Nome e Cor para exibir apenas um card por "Modelo"
        var produtosAgrupados = produtos
            .GroupBy(p => new { p.Nome, p.Cor })
            .Select(g => g.First())
            .ToList();

        var viewModel = new CatalogViewModel
        {
            Produtos = produtosAgrupados,
            Categorias = categorias,
            Tamanhos = await _api.GetTamanhosAsync(),
            CategoriaSelecionadaId = categoriaId,
            TamanhoSelecionadoId = tamanhoId,
            Busca = busca,
            Ordem = ordem
        };

        return View("Products", viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        // 1. Busca o produto principal via endpoint detalhado (garante todas as imagens)
        var produto = await _api.GetProdutoByIdAsync(id);
        
        if (produto == null) return NotFound();

        // 2. Busca todos os produtos para encontrar variantes (mesmo Nome/Cor)
        // Idealmente a API teria um endpoint /api/Produtos/{id}/Variantes
        var todosProdutos = await _api.GetProdutosAsync(null, null, null, null);
        
        var variantes = todosProdutos
            .Where(p => p.Nome == produto.Nome && p.Cor == produto.Cor && p.CategoriaId == produto.CategoriaId)
            .OrderBy(p => p.TamanhoId)
            .ToList();

        // 3. Busca avaliações (últimas 8) de TODAS as variantes do modelo
        var idsVariantes = variantes.Select(v => v.Id).ToList();
        if (!idsVariantes.Contains(id)) idsVariantes.Add(id); // Garante que o ID atual está na lista

        var avaliacoes = await _context.Avaliacoes
            .AsNoTracking()
            .Where(a => idsVariantes.Contains(a.ProdutoId))
            .OrderByDescending(a => a.DataAvaliacao)
            .Take(8)
            .Include(a => a.Usuario)
            .ToListAsync();

        var viewModel = new Vion.Web.Models.ViewModels.ProductDetailsViewModel
        {
            Produto = produto,
            Variantes = variantes,
            Avaliacoes = avaliacoes
        };

        return View(viewModel);
    }

    public IActionResult About()
    {
        return View();
    }
}
