using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Web.Services;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public PedidosController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Admin/Pedidos
        public async Task<IActionResult> Index(string status)
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

            query = query.OrderByDescending(p => p.DataPedido);

            ViewData["CurrentStatus"] = status;
            return View(await query.ToListAsync());
        }

        // GET: Admin/Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Admin/Pedidos/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            // Carregar pedido com Itens e Produto para o e-mail
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            // Validar status permitidos
            var statusPermitidos = new[] { "Pendente", "Aprovado", "Em Transporte", "Entregue", "Cancelado" };
            if (!statusPermitidos.Contains(status))
            {
                TempData["ErrorMessage"] = "Status inválido.";
                return RedirectToAction(nameof(Details), new { id });
            }

            pedido.Status = status;
            _context.Update(pedido);
            await _context.SaveChangesAsync();

            // Enviar e-mail de atualização
            await _emailService.SendOrderStatusUpdateEmailAsync(pedido, status);

            TempData["SuccessMessage"] = $"Status do pedido #{id} atualizado para {status}.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
