using Vion.Domain.Entities;
using Vion.Web.Models.Dtos;

namespace Vion.Web.Models.ViewModels;

public class ProductDetailsViewModel
{
    public ProdutoDto Produto { get; set; } = new();
    public List<ProdutoDto> Variantes { get; set; } = new();
    public List<Avaliacao> Avaliacoes { get; set; } = new();
    
    // Helper para saber se tem estoque de alguma variante
    public bool TemEstoque => Variantes.Any(v => v.Estoque > 0);
    
    // Lista de tamanhos ordenados
    public List<ProdutoDto> VariantesOrdenadas 
    {
        get 
        {
            // Tenta ordenar numericamente se for número, senão alfabeticamente (ou por ID se TamanhoId seguir ordem)
            // Aqui assumimos que TamanhoId segue uma lógica ou Tamanho nome.
            // Para simplicidade, ordenamos por Tamanho (nome) ou Id.
            return Variantes.OrderBy(v => v.TamanhoId).ToList();
        }
    }
}
