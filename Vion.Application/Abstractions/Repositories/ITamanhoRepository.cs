using Vion.Domain.Entities;

namespace Vion.Application.Abstractions.Repositories
{
    public interface ITamanhoRepository
    {
        Task<IEnumerable<Tamanho>> GetAllAsync();
        Task<Tamanho?> GetByIdAsync(int id);
        Task<Tamanho> CreateAsync(Tamanho tamanho);
        Task UpdateAsync(Tamanho tamanho);
        Task DeleteAsync(Tamanho tamanho);
    }
}
