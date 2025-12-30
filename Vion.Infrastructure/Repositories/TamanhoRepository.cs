using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Application.Abstractions.Repositories;

namespace Vion.Infrastructure.Repositories;

public class TamanhoRepository : ITamanhoRepository
{
    private readonly AppDbContext _context;

    public TamanhoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tamanho>> GetAllAsync()
    {
        return await _context.Tamanhos.ToListAsync();
    }
}
