using Microsoft.AspNetCore.Mvc;
using Vion.Application.Abstractions.Repositories;

namespace Vion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;

    public CategoriasController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }
}
