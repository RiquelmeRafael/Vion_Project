using Microsoft.AspNetCore.Mvc;
using Vion.Application.Abstractions.Repositories;

namespace Vion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TamanhosController : ControllerBase
{
    private readonly ITamanhoRepository _repository;

    public TamanhosController(ITamanhoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }
}
