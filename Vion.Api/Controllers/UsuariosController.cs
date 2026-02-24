using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Gerente")]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly AppDbContext _context;

        public UsuariosController(UserManager<Usuario> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarios()
        {
            var usuarios = await _context.Users
                .Include(u => u.TipoUsuario)
                .AsNoTracking()
                .Select(u => new
                {
                    u.Id,
                    u.Nome,
                    u.Email,
                    TipoUsuarioId = u.TipoUsuarioId,
                    TipoUsuario = u.TipoUsuario != null ? u.TipoUsuario.Nome : "N/A"
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUsuario(int id)
        {
            var usuario = await _context.Users
                .Include(u => u.TipoUsuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.TipoUsuarioId,
                TipoUsuario = usuario.TipoUsuario?.Nome
            });
        }

        // POST: api/usuarios
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUsuario([FromBody] CreateUsuarioDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                UserName = model.Email,
                TipoUsuarioId = model.TipoUsuarioId,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuario, model.Senha);

            if (result.Succeeded)
            {
                var tipo = await _context.TiposUsuario.FindAsync(model.TipoUsuarioId);
                if (tipo != null)
                {
                    await _userManager.AddToRoleAsync(usuario, tipo.Nome);
                }
                
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, new 
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.TipoUsuarioId,
                    TipoUsuario = tipo?.Nome
                });
            }

            return BadRequest(result.Errors);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioDto model)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario == null) return NotFound();

            usuario.Nome = model.Nome;
            usuario.Email = model.Email;
            usuario.UserName = model.Email; // Keep username synced with email

            // Se mudou o tipo de usuário, atualizar roles
            if (usuario.TipoUsuarioId != model.TipoUsuarioId)
            {
                // Remover roles atuais
                var roles = await _userManager.GetRolesAsync(usuario);
                await _userManager.RemoveFromRolesAsync(usuario, roles);

                // Adicionar nova role
                var novoTipo = await _context.TiposUsuario.FindAsync(model.TipoUsuarioId);
                if (novoTipo != null)
                {
                    await _userManager.AddToRoleAsync(usuario, novoTipo.Nome);
                }
                
                usuario.TipoUsuarioId = model.TipoUsuarioId;
            }

            // Se forneceu senha, alterar
            if (!string.IsNullOrEmpty(model.Senha))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var resultSenha = await _userManager.ResetPasswordAsync(usuario, token, model.Senha);
                if (!resultSenha.Succeeded) return BadRequest(resultSenha.Errors);
            }

            var result = await _userManager.UpdateAsync(usuario);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario == null) return NotFound();

            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpGet("tipos")]
        public async Task<ActionResult<IEnumerable<object>>> GetTiposUsuario()
        {
            var tipos = await _context.TiposUsuario.AsNoTracking().ToListAsync();
            return Ok(tipos);
        }

        [HttpGet("tipos-publico")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<object>>> GetTiposUsuarioPublico()
        {
            var tipos = await _context.TiposUsuario
                .Where(t => t.Nome == "Admin" || t.Nome == "Gerente")
                .AsNoTracking()
                .ToListAsync();
            return Ok(tipos);
        }
    }

    public class CreateUsuarioDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public int TipoUsuarioId { get; set; }
    }

    public class UpdateUsuarioDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string? Senha { get; set; } // Opcional na edição
        public int TipoUsuarioId { get; set; }
    }
}
