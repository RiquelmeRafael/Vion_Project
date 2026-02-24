using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Controllers
{
    [Authorize]
    public class AvaliacaoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IWebHostEnvironment _environment;

        public AvaliacaoController(AppDbContext context, UserManager<Usuario> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(int produtoId, int pedidoId, int nota, string comentario, IFormFile? imagem)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Validar se o pedido existe, pertence ao usuário e tem status Entregue
            var pedido = await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId && p.UsuarioId == user.Id);

            // Aceitamos status Entregue. O usuário disse "quando o pedido fosse entregue".
            if (pedido == null || pedido.Status != "Entregue")
            {
                TempData["Erro"] = "Você só pode avaliar produtos de pedidos entregues.";
                return RedirectToAction("MeusPedidos", "Checkout");
            }

            // Validar se o produto está no pedido
            if (!pedido.Itens.Any(i => i.ProdutoId == produtoId))
            {
                TempData["Erro"] = "Produto não encontrado neste pedido.";
                return RedirectToAction("MeusPedidos", "Checkout");
            }
            
            // Validar se já avaliou
            var jaAvaliou = await _context.Avaliacoes
                .AnyAsync(a => a.PedidoId == pedidoId && a.ProdutoId == produtoId && a.UsuarioId == user.Id);
            
            if (jaAvaliou)
            {
                 TempData["Erro"] = "Você já avaliou este produto.";
                 return RedirectToAction("MeusPedidos", "Checkout");
            }

            var avaliacao = new Avaliacao
            {
                ProdutoId = produtoId,
                PedidoId = pedidoId,
                UsuarioId = user.Id,
                Nota = nota,
                Comentario = comentario,
                DataAvaliacao = DateTime.UtcNow
            };

            if (imagem != null && imagem.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
                var path = Path.Combine(_environment.WebRootPath, "images", "avaliacoes");
                
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                
                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await imagem.CopyToAsync(stream);
                }
                
                avaliacao.ImagemUrl = $"/images/avaliacoes/{fileName}";
            }

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Avaliação enviada com sucesso! Obrigado pelo feedback.";
            return RedirectToAction("MeusPedidos", "Checkout");
        }
    }
}
