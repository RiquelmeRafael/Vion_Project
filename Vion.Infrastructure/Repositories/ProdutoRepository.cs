using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Application.Abstractions.Repositories;

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
        return await _context.Produtos.ToListAsync();
    }


    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Tamanho)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
