using Microsoft.EntityFrameworkCore;
using Vion.Application.Abstractions.Repositories;
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
            .ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Tamanho)
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

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
