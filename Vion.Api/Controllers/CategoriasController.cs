using Microsoft.AspNetCore.Mvc;
using Vion.Application.Abstractions.Repositories;
using Vion.Application.DTOs;
using Vion.Domain.Entities;

namespace Vion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        // =======================
        // GET ALL
        // =======================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _repository.GetAllAsync();

            var result = categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome
            });

            return Ok(result);
        }

        // =======================
        // GET BY ID
        // =======================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(new CategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            });
        }

        // =======================
        // POST
        // =======================
        [HttpPost]
        public async Task<IActionResult> Create(CategoriaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Nome é obrigatório.");

            var categoria = new Categoria
            {
                Nome = dto.Nome
            };

            await _repository.AddAsync(categoria);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = categoria.Id },
                new CategoriaDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                }
            );
        }

        // =======================
        // PUT
        // =======================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriaUpdateDto dto)
        {
            var categoria = await _repository.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Nome é obrigatório.");

            categoria.Nome = dto.Nome;

            _repository.Update(categoria);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // =======================
        // DELETE
        // =======================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            _repository.Delete(categoria);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
