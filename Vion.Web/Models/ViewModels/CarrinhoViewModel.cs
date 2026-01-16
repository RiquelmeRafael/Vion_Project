using Vion.Domain.Entities;

namespace Vion.Web.Models.ViewModels
{
    public class ItemCarrinhoViewModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public decimal? ValorFreteFixo { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public string? Tamanho { get; set; }
        public decimal Total => Preco * Quantidade;
    }

    public class CarrinhoViewModel
    {
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new List<ItemCarrinhoViewModel>();
        public decimal Total => Itens.Sum(i => i.Total);
        public decimal ValorFrete { get; set; }
        public int PrazoFreteDias { get; set; }
        public string? TipoFrete { get; set; }
        public decimal FreteFixoTotal => Itens
            .Where(i => i.ValorFreteFixo.HasValue)
            .Sum(i => i.ValorFreteFixo!.Value * i.Quantidade);

        public string? CupomAplicado { get; set; }
        public decimal ValorDesconto { get; set; }
        public string? MensagemCupom { get; set; }

        public decimal TotalFrete => ValorFrete + FreteFixoTotal;

        public decimal TotalComFrete => Total + TotalFrete - ValorDesconto;
    }
}
