using Microsoft.AspNetCore.Mvc;
using Vion.Application.Abstractions.Repositories;
using Vion.Application.DTOs;
using Vion.Domain.Entities;

namespace Vion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TamanhosController : ControllerBase
    {
        private readonly ITamanhoRepository _repository;

        public TamanhosController(ITamanhoRepository repository)
        {
            _repository = repository;
        }

        // GET api/tamanhos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tamanhos = await _repository.GetAllAsync();

            var result = tamanhos.Select(t => new TamanhoDto
            {
                Id = t.Id,
                Nome = t.Nome
            });

            return Ok(result);
        }

        // GET api/tamanhos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tamanho = await _repository.GetByIdAsync(id);
            if (tamanho == null)
                return NotFound();

            return Ok(new TamanhoDto
            {
                Id = tamanho.Id,
                Nome = tamanho.Nome
            });
        }

        // POST api/tamanhos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TamanhoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Nome é obrigatório.");

            var tamanho = new Tamanho
            {
                Nome = dto.Nome
            };

            await _repository.CreateAsync(tamanho);

            return CreatedAtAction(nameof(GetById), new { id = tamanho.Id }, new TamanhoDto
            {
                Id = tamanho.Id,
                Nome = tamanho.Nome
            });
        }

        // PUT api/tamanhos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TamanhoUpdateDto dto)
        {
            var tamanho = await _repository.GetByIdAsync(id);
            if (tamanho == null)
                return NotFound();

            tamanho.Nome = dto.Nome;
            await _repository.UpdateAsync(tamanho);

            return NoContent();
        }

        // DELETE api/tamanhos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tamanho = await _repository.GetByIdAsync(id);
            if (tamanho == null)
                return NotFound();

            await _repository.DeleteAsync(tamanho);
            return NoContent();
        }
    }
}
