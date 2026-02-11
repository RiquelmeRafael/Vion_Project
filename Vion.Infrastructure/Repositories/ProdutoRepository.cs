using Microsoft.EntityFrameworkCore;
using Vion.Application.Abstractions.Repositories;
using Vion.Application.Filters;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Tamanho)
            .Include(p => p.Cupom)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> GetAllAsync(ProdutoFilter filter)
    {
        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Tamanho)
            .Include(p => p.Cupom)
            .AsQueryable();

        if (filter.CategoriaId.HasValue)
        {
            query = query.Where(p => p.CategoriaId == filter.CategoriaId.Value);
        }

        if (filter.TamanhoId.HasValue)
        {
            query = query.Where(p => p.TamanhoId == filter.TamanhoId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Busca))
        {
            query = query.Where(p => p.Nome.Contains(filter.Busca) || (p.Descricao != null && p.Descricao.Contains(filter.Busca)));
        }

        // Ordenação
        query = filter.Ordem switch
        {
            "menor_preco" => query.OrderBy(p => p.Preco),
            "maior_preco" => query.OrderByDescending(p => p.Preco),
            "nome" => query.OrderBy(p => p.Nome),
            _ => query.OrderBy(p => p.Nome) // Padrão
        };

        // Paginação
        query = query.Skip((filter.Page - 1) * filter.PageSize)
                     .Take(filter.PageSize);

        return await query.ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Tamanho)
            .Include(p => p.Cupom)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
    }

    public void Update(Produto produto)
    {
        _context.Produtos.Update(produto);
    }

    public void Delete(Produto produto)
    {
        _context.Produtos.Remove(produto);
    }

    public async Task UpdateImagesForVariantsAsync(string nome, string cor, int categoriaId, string? img1, string? img2, string? img3, string? img4)
    {
        var variants = await _context.Produtos
            .Where(p => p.Nome == nome && p.Cor == cor && p.CategoriaId == categoriaId)
            .ToListAsync();

        foreach (var p in variants)
        {
            p.ImagemUrl = img1 ?? "";
            p.ImagemUrl2 = img2;
            p.ImagemUrl3 = img3;
            p.ImagemUrl4 = img4;
            // No need to call Update(), ChangeTracker handles it.
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
