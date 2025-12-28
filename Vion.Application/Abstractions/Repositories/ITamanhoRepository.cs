using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vion.Domain.Entities;

namespace Vion.Application.Abstractions.Repositories;

public interface ITamanhoRepository
{
    Task<IEnumerable<Tamanho>> GetAllAsync();
}