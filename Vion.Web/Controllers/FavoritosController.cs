using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Controllers
{
    [Authorize]
    public class FavoritosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public FavoritosController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var favoritos = await _context.Favoritos
                .Include(f => f.Produto)
                .Where(f => f.UsuarioId == user.Id)
                .Select(f => f.Produto)
                .ToListAsync();

            return View(favoritos);
        }

        public async Task<IActionResult> Toggle(int produtoId, string? returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.UsuarioId == user.Id && f.ProdutoId == produtoId);

            if (favorito != null)
            {
                _context.Favoritos.Remove(favorito);
                TempData["MensagemSucesso"] = "Produto removido dos favoritos.";
            }
            else
            {
                var produto = await _context.Produtos.FindAsync(produtoId);
                if (produto != null)
                {
                    _context.Favoritos.Add(new Favorito
                    {
                        UsuarioId = user.Id,
                        ProdutoId = produtoId
                    });
                    TempData["MensagemSucesso"] = "Produto adicionado aos favoritos!";
                }
            }

            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remover(int produtoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.UsuarioId == user.Id && f.ProdutoId == produtoId);

            if (favorito != null)
            {
                _context.Favoritos.Remove(favorito);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Produto removido dos favoritos.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
