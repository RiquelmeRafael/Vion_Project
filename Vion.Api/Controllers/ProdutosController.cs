using Microsoft.AspNetCore.Mvc;
using Vion.Application.Abstractions.Repositories;

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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var produtos = await _repository.GetAllAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }
}
