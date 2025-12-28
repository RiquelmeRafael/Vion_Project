using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vion.Application.DTOs;
using Vion.Application.Filters;

namespace Vion.Application.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> GetAsync(ProdutoFilter filter);
    Task<ProdutoDto?> GetByIdAsync(int id);
}