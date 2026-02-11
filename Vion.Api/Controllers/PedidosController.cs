using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin,Gerente")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPedidos(string? status = null)
        {
            var query = _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status == status);
            }

            var pedidos = await query.OrderByDescending(p => p.DataPedido).ToListAsync();

            // Retornar um objeto simplificado ou DTO se necessário para evitar ciclo de referência JSON,
            // ou confiar na configuração do JSON serializer (ReferenceLoopHandling.Ignore).
            // Aqui vamos projetar um anônimo para garantir.
            var resultado = pedidos.Select(p => new
            {
                p.Id,
                p.DataPedido,
                p.Status,
                Total = p.ValorTotal,
                p.FormaPagamento,
                EnderecoEntrega = p.Endereco,
                p.Cidade,
                p.Estado,
                p.Cep,
                Usuario = p.Usuario != null ? new { p.Usuario.Nome, p.Usuario.Email } : null,
                Itens = p.Itens.Select(i => new
                {
                    i.ProdutoId,
                    ProdutoNome = i.Produto?.Nome,
                    i.Quantidade,
                    i.PrecoUnitario
                })
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPedido(int id)
        {
            var p = await _context.Pedidos
                .Include(pedido => pedido.Usuario)
                .Include(pedido => pedido.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (p == null) return NotFound();

            return Ok(new
            {
                p.Id,
                p.DataPedido,
                p.Status,
                Total = p.ValorTotal,
                p.FormaPagamento,
                EnderecoEntrega = p.Endereco,
                p.Cidade,
                p.Estado,
                p.Cep,
                Usuario = p.Usuario != null ? new { p.Usuario.Nome, p.Usuario.Email } : null,
                Itens = p.Itens.Select(i => new
                {
                    i.ProdutoId,
                    ProdutoNome = i.Produto?.Nome,
                    i.Quantidade,
                    i.PrecoUnitario,
                    ImagemUrl = i.Produto?.ImagemUrl
                })
            });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string novoStatus)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            var statusPermitidos = new[] { "Pendente", "Aprovado", "Em Transporte", "Entregue", "Cancelado" };
            if (!statusPermitidos.Contains(novoStatus))
            {
                return BadRequest("Status inválido.");
            }

            pedido.Status = novoStatus;
            
            // TODO: Implementar envio de e-mail aqui se necessário (precisa de serviço de e-mail no API)
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
