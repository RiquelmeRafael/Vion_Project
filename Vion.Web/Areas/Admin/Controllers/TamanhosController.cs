using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class TamanhosController : Controller
    {
        private readonly AppDbContext _context;

        public TamanhosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Tamanhos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tamanhos.ToListAsync());
        }

        // GET: Admin/Tamanhos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanho = await _context.Tamanhos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamanho == null)
            {
                return NotFound();
            }

            return View(tamanho);
        }

        // GET: Admin/Tamanhos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tamanhos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Tamanho tamanho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tamanho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tamanho);
        }

        // GET: Admin/Tamanhos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanho = await _context.Tamanhos.FindAsync(id);
            if (tamanho == null)
            {
                return NotFound();
            }
            return View(tamanho);
        }

        // POST: Admin/Tamanhos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Tamanho tamanho)
        {
            if (id != tamanho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tamanho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TamanhoExists(tamanho.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tamanho);
        }

        // GET: Admin/Tamanhos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanho = await _context.Tamanhos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamanho == null)
            {
                return NotFound();
            }

            return View(tamanho);
        }

        // POST: Admin/Tamanhos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tamanho = await _context.Tamanhos.FindAsync(id);
            if (tamanho != null)
            {
                _context.Tamanhos.Remove(tamanho);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TamanhoExists(int id)
        {
            return _context.Tamanhos.Any(e => e.Id == id);
        }
    }
}
