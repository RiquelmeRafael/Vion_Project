using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
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
        private readonly IConfiguration _configuration;

        public PedidosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null) return NotFound();

            var statusPermitidos = new[] { "Pendente", "Aprovado", "Em Transporte", "Entregue", "Cancelado" };
            if (!statusPermitidos.Contains(novoStatus))
            {
                return BadRequest("Status inválido.");
            }

            pedido.Status = novoStatus;
            await _context.SaveChangesAsync();

            await SendOrderStatusUpdateEmailAsync(pedido, novoStatus);
            return NoContent();
        }

        private async Task SendOrderStatusUpdateEmailAsync(Pedido pedido, string novoStatus)
        {
            if (string.IsNullOrEmpty(pedido.EmailCliente))
            {
                return;
            }

            var baseUrl = _configuration["SiteSettings:BaseUrl"] ?? "https://localhost:5001";
            var pedidoUrl = $"{baseUrl.TrimEnd('/')}/Checkout/MeusPedidos";

            var subject = $"Vion - Atualização do Pedido #{pedido.Id}";
            var body = BuildStatusUpdateTemplate(pedido, novoStatus, pedidoUrl);

            var smtp = _configuration.GetSection("SmtpSettings");
            var host = smtp["Host"];
            var port = int.Parse(smtp["Port"] ?? "587");
            var senderEmail = smtp["SenderEmail"];
            var password = smtp["Password"];
            var enableSsl = bool.Parse(smtp["EnableSsl"] ?? "true");

            if (string.IsNullOrWhiteSpace(senderEmail) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(senderEmail, password),
                EnableSsl = enableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, "Vion Store"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.ReplyToList.Add(new MailAddress(senderEmail, "Vion Store"));
            mailMessage.To.Add(pedido.EmailCliente);

            await client.SendMailAsync(mailMessage);
        }

        private string BuildStatusUpdateTemplate(Pedido pedido, string novoStatus, string pedidoUrl)
        {
            return $@"
<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden;'>
    <div style='background-color: #000; color: #fff; padding: 20px; text-align: center;'>
        <h1 style='margin: 0;'>Atualização de Status</h1>
    </div>
    <div style='padding: 20px; background-color: #fff; color: #333;'>
        <p>Olá, <strong>{pedido.NomeCliente}</strong>!</p>
        <p>O seu pedido <strong>#{pedido.Id}</strong> teve uma atualização.</p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <p style='font-size: 1.1em; margin-bottom: 10px;'>Novo Status:</p>
            <span style='background-color: #007bff; color: white; padding: 10px 20px; border-radius: 20px; font-weight: bold; font-size: 1.2em;'>
                {novoStatus}
            </span>
        </div>

        <p>Você pode acompanhar os detalhes do seu pedido acessando sua conta em nosso site.</p>
        
        <div style='margin-top: 30px; text-align: center;'>
            <a href='{pedidoUrl}' style='background-color: #000; color: #fff; padding: 12px 25px; text-decoration: none; border-radius: 4px; font-weight: bold;'>Ver Pedido</a>
        </div>
    </div>
    <div style='background-color: #f0f0f0; padding: 15px; text-align: center; font-size: 0.8em; color: #666;'>
        <p>&copy; {DateTime.Now.Year} Vion. Todos os direitos reservados.</p>
    </div>
</div>";
        }
    }
}
