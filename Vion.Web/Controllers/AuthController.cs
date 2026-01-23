﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Web.Extensions;
using Vion.Web.Models;
using Vion.Web.Models.ViewModels;
using Vion.Web.Services;

namespace Vion.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private const string SessaoCarrinho = "Carrinho";

        public AuthController(
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            IEmailService emailService,
            AppDbContext context,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginVm { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

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

            if (user != null)
            {
                // Merge do carrinho da sessão para o banco
                var carrinhoSessao = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
                if (carrinhoSessao != null && carrinhoSessao.Itens.Any())
                {
                    var itensBanco = await _context.CarrinhoItens
                        .Where(c => c.UsuarioId == user.Id)
                        .ToListAsync();

                    foreach (var itemSessao in carrinhoSessao.Itens)
                    {
                        var itemExistente = itensBanco.FirstOrDefault(i => i.ProdutoId == itemSessao.ProdutoId);
                        if (itemExistente != null)
                        {
                            itemExistente.Quantidade += itemSessao.Quantidade;
                        }
                        else
                        {
                            _context.CarrinhoItens.Add(new CarrinhoItem
                            {
                                UsuarioId = user.Id,
                                ProdutoId = itemSessao.ProdutoId,
                                Quantidade = itemSessao.Quantidade,
                                DataAdicionado = DateTime.UtcNow
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                    
                    // Limpa sessão pois agora está no banco
                    HttpContext.Session.Remove(SessaoCarrinho);
                }
            }

            if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // Limpa qualquer dado de carrinho da sessão atual para evitar confusão
            HttpContext.Session.Remove(SessaoCarrinho);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var tipoCliente = await _context.TiposUsuario.FirstOrDefaultAsync(t => t.Nome == "Cliente");
            if (tipoCliente == null)
            {
                ModelState.AddModelError("", "Tipo de usuário padrão 'Cliente' não encontrado.");
                return View(vm);
            }

            var user = new Usuario
            {
                UserName = vm.Email,
                Email = vm.Email,
                Nome = vm.Nome,
                TipoUsuarioId = tipoCliente.Id
            };

            var result = await _userManager.CreateAsync(user, vm.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Cliente");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetPath = Url.Action("ResetPassword", "Auth",
                    new { token, email = user.Email }) ?? "";

                var baseUrl = _configuration["SiteSettings:BaseUrl"];
                string callbackUrl;

                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    callbackUrl = $"{baseUrl!.TrimEnd('/')}{resetPath}";
                }
                else
                {
                    callbackUrl = Url.Action("ResetPassword", "Auth",
                        new { token, email = user.Email }, protocol: Request.Scheme)
                        ?? resetPath;
                }

                if (user.Email != null && !string.IsNullOrEmpty(callbackUrl))
                {
                    await _emailService.SendPasswordResetEmailAsync(user.Email, callbackUrl);
                }
            }

            ViewData["ForgotPasswordSent"] = true;
            return View(vm);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Token inválido");
            }
            return View(new ResetPasswordVm { Token = token ?? "", Email = email ?? "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                TempData["PasswordResetSuccess"] = true;
                return RedirectToAction(nameof(Login));
            }

            var result = await _userManager.ResetPasswordAsync(user, vm.Token, vm.NovaSenha);
            if (result.Succeeded)
            {
                TempData["PasswordResetSuccess"] = true;
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
