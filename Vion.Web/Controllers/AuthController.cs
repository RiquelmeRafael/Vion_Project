using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vion.Domain.Entities;
using Vion.Web.Models;

namespace Vion.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;

        public AuthController(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _signInManager.PasswordSignInAsync(
                vm.Email,
                vm.Senha,
                false,
                false
            );

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login inválido");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
