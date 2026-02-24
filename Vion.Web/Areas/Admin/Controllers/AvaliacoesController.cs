using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class AvaliacoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AvaliacoesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _context.Avaliacoes
                .AsNoTracking()
                .Include(a => a.Produto)
                .Include(a => a.Usuario)
                .OrderByDescending(a => a.DataAvaliacao)
                .ToListAsync();

            return View(avaliacoes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                if (!string.IsNullOrEmpty(avaliacao.ImagemUrl))
                {
                    var relativePath = avaliacao.ImagemUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                    var fullPath = Path.Combine(_environment.WebRootPath, relativePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                _context.Avaliacoes.Remove(avaliacao);
                await _context.SaveChangesAsync();
            }

            TempData["MensagemSucesso"] = "Avaliação removida com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}

