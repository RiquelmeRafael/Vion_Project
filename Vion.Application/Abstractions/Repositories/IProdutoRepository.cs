using Vion.Application.Filters;
using Vion.Domain.Entities;

namespace Vion.Application.Abstractions.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<IEnumerable<Produto>> GetAllAsync(ProdutoFilter filter);
    Task<Produto?> GetByIdAsync(int id);

    Task AddAsync(Produto produto);
    void Update(Produto produto);
    void Delete(Produto produto);

    Task UpdateImagesForVariantsAsync(string nome, string cor, int categoriaId, string? img1, string? img2, string? img3, string? img4);

    Task<bool> SaveChangesAsync();
}
