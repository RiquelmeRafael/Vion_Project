using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IConfiguration configuration,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Senha))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            // Verificar Roles
            var roles = await _userManager.GetRolesAsync(user);
            
            // Lógica simples: Pega a primeira role de maior privilégio ou apenas a primeira encontrada
            // Para o Desktop, só permitimos Admin ou Gerente
            var role = roles.FirstOrDefault(r => r == "Admin" || r == "Gerente");

            if (role == null)
            {
                return Forbid("Acesso negado. Apenas Administradores ou Gerentes podem acessar o sistema Desktop.");
            }

            var token = GenerateJwtToken(user, role);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Role = role,
                Email = user.Email,
                Nome = user.Nome
            });
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tipo = await _context.TiposUsuario.FirstOrDefaultAsync(t => t.Id == dto.TipoUsuarioId);
            if (tipo == null) return BadRequest("Tipo de usuário inválido.");

            if (tipo.Nome != "Admin" && tipo.Nome != "Gerente")
            {
                return BadRequest("Somente tipos Admin ou Gerente podem ser cadastrados por aqui.");
            }

            var existing = await _userManager.FindByEmailAsync(dto.Email);
            if (existing != null) return BadRequest("Já existe um usuário com esse e-mail.");

            var user = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                UserName = dto.Email,
                TipoUsuarioId = dto.TipoUsuarioId,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Senha);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, tipo.Nome);

            return Ok();
        }

        private string GenerateJwtToken(Usuario user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
            {
                throw new InvalidOperationException("Jwt:Key must be configured and have at least 32 characters.");
            }

            var key = Encoding.ASCII.GetBytes(jwtKey);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }

    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public required string Role { get; set; }
        public required string? Email { get; set; }
        public required string? Nome { get; set; }
    }

    public class RegisterAdminDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}
