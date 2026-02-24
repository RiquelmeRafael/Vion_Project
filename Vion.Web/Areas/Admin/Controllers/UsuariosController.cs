using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _context;

        public UsuariosController(
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Admin/Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Users
                .Include(u => u.TipoUsuario)
                .AsNoTracking()
                .ToListAsync();
            return View(usuarios);
        }

        // GET: Admin/Usuarios/Create
        public IActionResult Create()
        {
            var tiposAdminGerente = _context.TiposUsuario
                .Where(t => t.Nome == "Admin" || t.Nome == "Gerente")
                .ToList();

            ViewData["TipoUsuarioId"] = new SelectList(tiposAdminGerente, "Id", "Nome");
            return View();
        }

        // POST: Admin/Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario, string Senha)
        {
            // Remover validações de campos que não vêm do formulário
            ModelState.Remove("TipoUsuario");
            ModelState.Remove("UserName");
            ModelState.Remove("Id");
            ModelState.Remove("PasswordHash");

            if (string.IsNullOrWhiteSpace(Senha))
            {
                ModelState.AddModelError("Senha", "A senha é obrigatória.");
            }

            if (ModelState.IsValid)
            {
                // Configurar username igual email
                usuario.UserName = usuario.Email;
                usuario.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(usuario, Senha);
                if (result.Succeeded)
                {
                    // Vincular Role baseado no TipoUsuario
                    var tipo = await _context.TiposUsuario.FindAsync(usuario.TipoUsuarioId);
                    if (tipo != null)
                    {
                        await _userManager.AddToRoleAsync(usuario, tipo.Nome);
                    }

                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            var tiposAdminGerente = _context.TiposUsuario
                .Where(t => t.Nome == "Admin" || t.Nome == "Gerente")
                .ToList();

            ViewData["TipoUsuarioId"] = new SelectList(tiposAdminGerente, "Id", "Nome", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // GET: Admin/Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "Id", "Nome", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // POST: Admin/Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();

            // Remover validações de campos que não vêm do formulário
            ModelState.Remove("TipoUsuario");
            ModelState.Remove("UserName");
            ModelState.Remove("Senha"); // Senha não é editada aqui

            // Buscar usuário original para manter dados sensíveis
            var userDb = await _userManager.FindByIdAsync(id.ToString());
            if (userDb == null) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualizar campos permitidos
                    userDb.Nome = usuario.Nome;
                    userDb.Email = usuario.Email;
                    userDb.UserName = usuario.Email;
                    
                    // Verificar se mudou o tipo de usuário
                    if (userDb.TipoUsuarioId != usuario.TipoUsuarioId)
                    {
                        // Remover roles antigas
                        var roles = await _userManager.GetRolesAsync(userDb);
                        await _userManager.RemoveFromRolesAsync(userDb, roles);

                        // Adicionar nova role
                        userDb.TipoUsuarioId = usuario.TipoUsuarioId;
                        var novoTipo = await _context.TiposUsuario.FindAsync(usuario.TipoUsuarioId);
                        if (novoTipo != null)
                        {
                            await _userManager.AddToRoleAsync(userDb, novoTipo.Nome);
                        }
                    }

                    var result = await _userManager.UpdateAsync(userDb);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "Id", "Nome", usuario.TipoUsuarioId);
                        return View(usuario);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "Id", "Nome", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // GET: Admin/Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Users
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Admin/Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario != null)
            {
                // Soft Delete: Bloquear usuário por 100 anos
                await _userManager.SetLockoutEndDateAsync(usuario, DateTimeOffset.UtcNow.AddYears(100));
                
                // Opcional: Desativar campo Ativo se existisse, mas Lockout resolve para login
            }
            return RedirectToAction(nameof(Index));
        }
        
        // POST: Admin/Usuarios/Reativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());
            if (usuario != null)
            {
                await _userManager.SetLockoutEndDateAsync(usuario, null);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
