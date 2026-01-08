using Microsoft.AspNetCore.Mvc;
using Vion.Application.Abstractions.Repositories;
using Vion.Application.DTOs;
using Vion.Application.Filters;
using Vion.Domain.Entities;

namespace Vion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;

    public ProdutosController(IProdutoRepository repository)
    {
        _repository = repository;
    }

    // =======================
    // GET ALL
    // =======================
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ProdutoFilter filter)
    {
        var produtos = await _repository.GetAllAsync(filter);

        var response = produtos.Select(p => new
        {
            p.Id,
            p.Nome,
            p.Descricao,
            p.Preco,
            Categoria = p.Categoria.Nome,
            Tamanho = p.Tamanho.Nome,
            p.Estoque,
            p.ImagemUrl
        });

        return Ok(response);
    }

    // =======================
    // GET BY ID
    // =======================
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null)
            return NotFound();

        return Ok(new
        {
            produto.Id,
            produto.Nome,
            produto.Descricao,
            produto.Preco,
            Categoria = produto.Categoria.Nome,
            Tamanho = produto.Tamanho.Nome,
            produto.Cor,
            produto.Estoque,
            produto.ImagemUrl
        });
    }

    // =======================
    // POST
    // =======================
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProdutoCreateDto dto)
    {
        var produto = new Produto
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Preco = dto.Preco,
            CategoriaId = dto.CategoriaId,
            TamanhoId = dto.TamanhoId,
            Cor = dto.Cor,
            Estoque = dto.Estoque,
            ImagemUrl = dto.ImagemUrl,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(produto);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto.Id);
    }

    // =======================
    // PUT
    // =======================
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProdutoUpdateDto dto)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null)
            return NotFound();

        produto.Nome = dto.Nome;
        produto.Descricao = dto.Descricao;
        produto.Preco = dto.Preco;
        produto.CategoriaId = dto.CategoriaId;
        produto.TamanhoId = dto.TamanhoId;
        produto.Cor = dto.Cor;
        produto.Estoque = dto.Estoque;
        produto.ImagemUrl = dto.ImagemUrl;

        _repository.Update(produto);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

    // =======================
    // DELETE
    // =======================
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null)
            return NotFound();

        _repository.Delete(produto);
        await _repository.SaveChangesAsync();

        return NoContent();
    }
}
