using Vion.Domain.Entities;

namespace Vion.Web.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalPedidos { get; set; }
        public int TotalProdutos { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalTamanhos { get; set; }
        public int TotalUsuarios { get; set; }
        
        public IEnumerable<Produto> UltimosProdutos { get; set; } = new List<Produto>();
        
        // Dados para Gráficos
        public Dictionary<string, int> ProdutosPorCategoria { get; set; } = new();
    }
}
