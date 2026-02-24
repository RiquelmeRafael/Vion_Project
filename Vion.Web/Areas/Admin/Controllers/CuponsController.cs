using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class CuponsController : Controller
    {
        private readonly AppDbContext _context;

        public CuponsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Cupons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cupons.AsNoTracking().ToListAsync());
        }

        // GET: Admin/Cupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cupons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,PercentualDesconto,Ativo")] Cupom cupom)
        {
            if (ModelState.IsValid)
            {
                // Verificar se já existe código igual
                if (await _context.Cupons.AnyAsync(c => c.Codigo == cupom.Codigo))
                {
                    ModelState.AddModelError("Codigo", "Este código de cupom já existe.");
                    return View(cupom);
                }

                _context.Add(cupom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cupom);
        }

        // GET: Admin/Cupons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null) return NotFound();

            return View(cupom);
        }

        // POST: Admin/Cupons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,PercentualDesconto,Ativo")] Cupom cupom)
        {
            if (id != cupom.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar duplicidade se alterou o código
                    if (await _context.Cupons.AnyAsync(c => c.Codigo == cupom.Codigo && c.Id != id))
                    {
                        ModelState.AddModelError("Codigo", "Este código de cupom já existe.");
                        return View(cupom);
                    }

                    _context.Update(cupom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CupomExists(cupom.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cupom);
        }

        // GET: Admin/Cupons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cupom = await _context.Cupons
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cupom == null) return NotFound();

            return View(cupom);
        }

        // POST: Admin/Cupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom != null)
            {
                // Desvincular produtos que usam este cupom para evitar erro de FK
                var produtos = await _context.Produtos.Where(p => p.CupomId == id).ToListAsync();
                foreach (var produto in produtos)
                {
                    produto.CupomId = null;
                }

                _context.Cupons.Remove(cupom);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CupomExists(int id)
        {
            return _context.Cupons.Any(e => e.Id == id);
        }
    }
}
