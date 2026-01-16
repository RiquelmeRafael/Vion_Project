using Microsoft.EntityFrameworkCore;
using Vion.Infrastructure.Persistence;
using Vion.Web.Areas.Admin.Models;

namespace Vion.Web.Areas.Admin.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var totalPedidos = await _context.Pedidos.CountAsync();
            var totalProdutos = await _context.Produtos.CountAsync();
            var totalCategorias = await _context.Categorias.CountAsync();
            var totalTamanhos = await _context.Tamanhos.CountAsync();
            var totalUsuarios = await _context.Users.CountAsync();

            var ultimosProdutos = await _context.Produtos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.Id)
                .Take(5)
                .ToListAsync();

            var produtosPorCategoria = await _context.Produtos
                .Include(p => p.Categoria)
                .GroupBy(p => p.Categoria != null ? p.Categoria.Nome : "Sem Categoria")
                .Select(g => new { Categoria = g.Key, Qtd = g.Count() })
                .ToDictionaryAsync(x => x.Categoria, x => x.Qtd);

            return new DashboardViewModel
            {
                TotalPedidos = totalPedidos,
                TotalProdutos = totalProdutos,
                TotalCategorias = totalCategorias,
                TotalTamanhos = totalTamanhos,
                TotalUsuarios = totalUsuarios,
                UltimosProdutos = ultimosProdutos,
                ProdutosPorCategoria = produtosPorCategoria
            };
        }
    }
}
