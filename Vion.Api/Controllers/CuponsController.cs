using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin,Gerente")] // Descomentar quando autenticação estiver configurada globalmente
    public class CuponsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CuponsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupom>>> GetCupons()
        {
            return await _context.Cupons.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cupom>> GetCupom(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null) return NotFound();
            return cupom;
        }

        [HttpPost]
        public async Task<ActionResult<Cupom>> CreateCupom(Cupom cupom)
        {
            if (await _context.Cupons.AnyAsync(c => c.Codigo == cupom.Codigo))
            {
                return BadRequest("Este código de cupom já existe.");
            }

            _context.Cupons.Add(cupom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCupom), new { id = cupom.Id }, cupom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCupom(int id, Cupom cupom)
        {
            if (id != cupom.Id) return BadRequest();

            if (await _context.Cupons.AnyAsync(c => c.Codigo == cupom.Codigo && c.Id != id))
            {
                return BadRequest("Este código de cupom já existe.");
            }

            _context.Entry(cupom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cupons.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupom(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null) return NotFound();

            // Desvincular produtos que usam este cupom
            var produtos = await _context.Produtos.Where(p => p.CupomId == id).ToListAsync();
            foreach (var produto in produtos)
            {
                produto.CupomId = null;
            }

            _context.Cupons.Remove(cupom);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
