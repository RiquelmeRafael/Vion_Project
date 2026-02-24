using Vion.Application.Abstractions.Repositories;
using Vion.Application.Filters;
using Vion.Web.Models.Dtos;

namespace Vion.Web.Services;

public class DirectApiClient : IApiClient
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly ITamanhoRepository _tamanhoRepository;

    public DirectApiClient(
        IProdutoRepository produtoRepository,
        ICategoriaRepository categoriaRepository,
        ITamanhoRepository tamanhoRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _tamanhoRepository = tamanhoRepository;
    }

    public async Task<IEnumerable<ProdutoDto>> GetProdutosAsync(
        int? categoriaId = null,
        int? tamanhoId = null,
        string? busca = null,
        string? ordem = null)
    {
        var filter = new ProdutoFilter
        {
            CategoriaId = categoriaId,
            TamanhoId = tamanhoId,
            Busca = busca,
            Ordem = ordem,
            PageSize = 100 // Ou outro valor adequado para o catálogo completo
        };

        var produtos = await _produtoRepository.GetAllAsync(filter);

        return produtos.Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            CategoriaId = p.CategoriaId,
            Categoria = p.Categoria?.Nome ?? "",
            TamanhoId = p.TamanhoId,
            Tamanho = p.Tamanho?.Nome ?? "",
            Cor = p.Cor,
            Estoque = p.Estoque,
            ValorFreteFixo = p.ValorFreteFixo,
            ImagemUrl = p.ImagemUrl,
            ImagemUrl2 = p.ImagemUrl2,
            ImagemUrl3 = p.ImagemUrl3,
            ImagemUrl4 = p.ImagemUrl4,
            CupomId = p.CupomId,
            CupomCodigo = p.Cupom?.Codigo
        });
    }

    public async Task<ProdutoDto?> GetProdutoByIdAsync(int id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null) return null;

        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            CategoriaId = produto.CategoriaId,
            Categoria = produto.Categoria?.Nome ?? "",
            TamanhoId = produto.TamanhoId,
            Tamanho = produto.Tamanho?.Nome ?? "",
            Cor = produto.Cor,
            Estoque = produto.Estoque,
            ValorFreteFixo = produto.ValorFreteFixo,
            ImagemUrl = produto.ImagemUrl,
            ImagemUrl2 = produto.ImagemUrl2,
            ImagemUrl3 = produto.ImagemUrl3,
            ImagemUrl4 = produto.ImagemUrl4,
            CupomId = produto.CupomId,
            CupomCodigo = produto.Cupom?.Codigo
        };
    }

    public async Task<IEnumerable<CategoriaDto>> GetCategoriasAsync()
    {
        var categorias = await _categoriaRepository.GetAllAsync();
        return categorias.Select(c => new CategoriaDto
        {
            Id = c.Id,
            Nome = c.Nome
        });
    }

    public async Task<IEnumerable<TamanhoDto>> GetTamanhosAsync()
    {
        var tamanhos = await _tamanhoRepository.GetAllAsync();
        return tamanhos.Select(t => new TamanhoDto
        {
            Id = t.Id,
            Nome = t.Nome
        });
    }
}
