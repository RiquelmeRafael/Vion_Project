using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vion.Domain.Entities;
using Vion.Web.Models.ViewModels;

namespace Vion.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public PerfilController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Auth");

            var model = new PerfilIndexViewModel
            {
                Dados = new PerfilViewModel
                {
                    Nome = user.Nome,
                    Email = user.Email ?? "",
                    Telefone = user.PhoneNumber
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarDados(PerfilIndexViewModel model)
        {
            // Clear validation for the other part
            ModelState.Remove("Senha.SenhaAtual");
            ModelState.Remove("Senha.NovaSenha");
            ModelState.Remove("Senha.ConfirmarNovaSenha");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // Update fields
            user.Nome = model.Dados.Nome;
            user.PhoneNumber = model.Dados.Telefone;

            if (model.Dados.Email != user.Email)
            {
                var emailResult = await _userManager.SetEmailAsync(user, model.Dados.Email);
                if (!emailResult.Succeeded)
                {
                    foreach (var error in emailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Index", model);
                }
                
                // Update UserName as well if it's the same
                await _userManager.SetUserNameAsync(user, model.Dados.Email);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                // Refresh sign-in to persist changes (like security stamp if email changed)
                await _signInManager.RefreshSignInAsync(user);
                TempData["Sucesso"] = "Dados atualizados com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(PerfilIndexViewModel model)
        {
            // Clear validation for the other part
            ModelState.Remove("Dados.Nome");
            ModelState.Remove("Dados.Email");
            ModelState.Remove("Dados.Telefone");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Auth");

            // Repopulate user data for the view
            model.Dados = new PerfilViewModel
            {
                Nome = user.Nome,
                Email = user.Email ?? "",
                Telefone = user.PhoneNumber
            };

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.Senha.SenhaAtual, model.Senha.NovaSenha);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Sucesso"] = "Senha alterada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index", model);
        }
    }
}
