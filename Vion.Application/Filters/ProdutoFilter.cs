using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Vion.Application.Filters;

public class ProdutoFilter
{
    public int? CategoriaId { get; set; }
    public int? TamanhoId { get; set; }
    public string? Busca { get; set; }
    public string? Ordem { get; set; } // "menor_preco", "maior_preco", "nome"

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}