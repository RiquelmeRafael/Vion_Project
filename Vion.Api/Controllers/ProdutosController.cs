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

        var response = produtos
            .OrderBy(p => p.Id)
            .Select(p => new
            {
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco,
                Categoria = p.Categoria?.Nome,
                Tamanho = p.Tamanho?.Nome,
                p.Cor,
                p.Estoque,
                p.ImagemUrl,
                p.ImagemUrl2,
                p.ImagemUrl3,
                p.ImagemUrl4,
                p.ValorFreteFixo,
                p.CupomId,
                CupomCodigo = p.Cupom != null && p.Cupom.Ativo ? p.Cupom.Codigo : null
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
            produto.CategoriaId,
            Categoria = produto.Categoria?.Nome,
            produto.TamanhoId,
            Tamanho = produto.Tamanho?.Nome,
            produto.Cor,
            produto.Estoque,
            produto.ImagemUrl,
            produto.ImagemUrl2,
            produto.ImagemUrl3,
            produto.ImagemUrl4,
            produto.ValorFreteFixo,
            produto.CupomId,
            CupomCodigo = produto.Cupom != null && produto.Cupom.Ativo ? produto.Cupom.Codigo : null
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
            ImagemUrl = dto.ImagemUrl ?? string.Empty,
            ImagemUrl2 = string.IsNullOrWhiteSpace(dto.ImagemUrl2) ? null : dto.ImagemUrl2,
            ImagemUrl3 = string.IsNullOrWhiteSpace(dto.ImagemUrl3) ? null : dto.ImagemUrl3,
            ImagemUrl4 = string.IsNullOrWhiteSpace(dto.ImagemUrl4) ? null : dto.ImagemUrl4,
            CupomId = dto.CupomId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(produto);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto.Id);
    }

    // =======================
    // PUT (CORRIGIDO � SEM REMOVER NADA)
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
        produto.Cor = dto.Cor;
        produto.Estoque = dto.Estoque;
        produto.ImagemUrl = dto.ImagemUrl ?? "";
        produto.ImagemUrl2 = string.IsNullOrWhiteSpace(dto.ImagemUrl2) ? null : dto.ImagemUrl2;
        produto.ImagemUrl3 = string.IsNullOrWhiteSpace(dto.ImagemUrl3) ? null : dto.ImagemUrl3;
        produto.ImagemUrl4 = string.IsNullOrWhiteSpace(dto.ImagemUrl4) ? null : dto.ImagemUrl4;
        
        if (dto.CupomId.HasValue)
            produto.CupomId = dto.CupomId;
        else
            produto.CupomId = null; // Permite remover o cupom

        // ?? PROTEO CONTRA FK INVLIDAINV�LIDA
        if (dto.CategoriaId > 0)
            produto.CategoriaId = dto.CategoriaId;

        if (dto.TamanhoId > 0)
            produto.TamanhoId = dto.TamanhoId;

        // Atualizar imagens de todas as variantes (mesmo Nome, Cor e Categoria)
        await _repository.UpdateImagesForVariantsAsync(
            produto.Nome,
            produto.Cor,
            produto.CategoriaId,
            produto.ImagemUrl,
            produto.ImagemUrl2,
            produto.ImagemUrl3,
            produto.ImagemUrl4
        );

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
