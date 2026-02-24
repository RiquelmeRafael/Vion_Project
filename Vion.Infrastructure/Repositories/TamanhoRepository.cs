using Microsoft.EntityFrameworkCore;
using Vion.Application.Abstractions.Repositories;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Infrastructure.Repositories
{
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

        public async Task<Tamanho?> GetByIdAsync(int id)
        {
            return await _context.Tamanhos.FindAsync(id);
        }

        public async Task<Tamanho> CreateAsync(Tamanho tamanho)
        {
            _context.Tamanhos.Add(tamanho);
            await _context.SaveChangesAsync();
            return tamanho;
        }

        public async Task UpdateAsync(Tamanho tamanho)
        {
            _context.Tamanhos.Update(tamanho);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tamanho tamanho)
        {
            _context.Tamanhos.Remove(tamanho);
            await _context.SaveChangesAsync();
        }
    }
}
