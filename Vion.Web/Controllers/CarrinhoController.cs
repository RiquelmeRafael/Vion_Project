using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Web.Extensions;
using Vion.Web.Models.ViewModels;
using Vion.Web.Services;

namespace Vion.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFreteService _freteService;
        private readonly UserManager<Usuario> _userManager;
        private const string SessaoCarrinho = "Carrinho";

        public CarrinhoController(AppDbContext context, IFreteService freteService, UserManager<Usuario> userManager)
        {
            _context = context;
            _freteService = freteService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var carrinho = await ObterCarrinhoAtualizadoAsync();
            return View(carrinho);
        }

        [HttpPost]
        public async Task<IActionResult> CalcularFrete(string cep)
        {
            var carrinho = await ObterCarrinhoAtualizadoAsync();
            if (carrinho.Itens.Any())
            {
                var resultado = await _freteService.CalcularFreteAsync(cep, carrinho.Total);
                
                if (resultado.Erro == null)
                {
                    carrinho.ValorFrete = resultado.Valor;
                    carrinho.PrazoFreteDias = resultado.PrazoDias;
                    carrinho.TipoFrete = resultado.Tipo;
                    await SalvarCarrinhoAsync(carrinho);
                }

                return Json(resultado);
            }
            return Json(new { erro = "Carrinho vazio" });
        }

        [HttpPost]
        public async Task<IActionResult> CalcularFreteProduto(string cep, string valorProduto, string? valorFreteFixo = null)
        {
            decimal valor = 0;
            if (!string.IsNullOrEmpty(valorProduto))
            {
                var valStr = valorProduto.Replace(",", ".");
                decimal.TryParse(valStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valor);
            }

            var resultado = await _freteService.CalcularFreteAsync(cep, valor);

            if (!string.IsNullOrEmpty(valorFreteFixo))
            {
                var valStr = valorFreteFixo.Replace(",", ".");
                if (decimal.TryParse(valStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal freteFixo))
                {
                    resultado.Valor = freteFixo;
                }
            }

            return Json(resultado);
        }

        // [Authorize] // Removido para permitir carrinho anônimo (Sessão)
        public async Task<IActionResult> Adicionar(int id, int quantidade = 1)
        {
            if (id <= 0)
            {
                TempData["MensagemErro"] = "Por favor, selecione um tamanho antes de adicionar ao carrinho.";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
                return RedirectToAction("Index", "Home");
            }

            var produto = await _context.Produtos
                .Include(p => p.Tamanho)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                TempData["MensagemErro"] = "Produto/Tamanho não encontrado.";
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            
            if (user != null)
            {
                // Lógica Persistente (Banco de Dados)
                var itemExistente = await _context.CarrinhoItens
                    .FirstOrDefaultAsync(c => c.UsuarioId == user.Id && c.ProdutoId == id);

                if (itemExistente != null)
                {
                    itemExistente.Quantidade += quantidade;
                    _context.CarrinhoItens.Update(itemExistente);
                }
                else
                {
                    var novoItem = new CarrinhoItem
                    {
                        UsuarioId = user.Id,
                        ProdutoId = id,
                        Quantidade = quantidade,
                        DataAdicionado = DateTime.UtcNow
                    };
                    _context.CarrinhoItens.Add(novoItem);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                // Lógica Sessão (Anônimo)
                var carrinho = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho) ?? new CarrinhoViewModel();
                var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id);

                if (itemExistente != null)
                {
                    itemExistente.Quantidade += quantidade;
                    if (!itemExistente.ValorFreteFixo.HasValue)
                        itemExistente.ValorFreteFixo = produto.ValorFreteFixo;
                }
                else
                {
                    carrinho.Itens.Add(new ItemCarrinhoViewModel
                    {
                        ProdutoId = produto.Id,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Quantidade = quantidade,
                        ImagemUrl = produto.ImagemUrl,
                        ValorFreteFixo = produto.ValorFreteFixo,
                        Tamanho = produto.Tamanho?.Nome // Exibe o tamanho no carrinho se disponível
                    });
                }
                
                carrinho.ValorFrete = 0; // Reseta frete ao mudar itens
                await CalcularDescontoAsync(carrinho);
                HttpContext.Session.Set(SessaoCarrinho, carrinho);
            }
            
            TempData["MensagemSucesso"] = $"{produto.Nome} adicionado ao carrinho!";
            
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer) && !referer.Contains("/Auth/Login"))
            {
                return Redirect(referer);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Esvaziar()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var itens = await _context.CarrinhoItens.Where(c => c.UsuarioId == user.Id).ToListAsync();
                if (itens.Any())
                {
                    _context.CarrinhoItens.RemoveRange(itens);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var carrinho = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
                if (carrinho != null)
                {
                    carrinho.Itens.Clear();
                    carrinho.ValorFrete = 0;
                    carrinho.ValorDesconto = 0;
                    carrinho.CupomAplicado = null;
                    HttpContext.Session.Set(SessaoCarrinho, carrinho);
                }
            }
            TempData["MensagemSucesso"] = "Carrinho esvaziado!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remover(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var item = await _context.CarrinhoItens
                    .FirstOrDefaultAsync(c => c.UsuarioId == user.Id && c.ProdutoId == id);
                
                if (item != null)
                {
                    _context.CarrinhoItens.Remove(item);
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Produto removido do carrinho!";
                }
            }
            else
            {
                var carrinho = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
                if (carrinho != null)
                {
                    var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id);
                    if (item != null)
                    {
                        carrinho.Itens.Remove(item);
                        carrinho.ValorFrete = 0;
                        await CalcularDescontoAsync(carrinho);
                        HttpContext.Session.Set(SessaoCarrinho, carrinho);
                        TempData["MensagemSucesso"] = "Produto removido do carrinho!";
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarQuantidade(int id, int quantidade)
        {
            if (quantidade <= 0) return await Remover(id);

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var item = await _context.CarrinhoItens
                    .FirstOrDefaultAsync(c => c.UsuarioId == user.Id && c.ProdutoId == id);
                
                if (item != null)
                {
                    item.Quantidade = quantidade;
                    _context.CarrinhoItens.Update(item);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var carrinho = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
                if (carrinho != null)
                {
                    var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id);
                    if (item != null)
                    {
                        item.Quantidade = quantidade;
                        carrinho.ValorFrete = 0;
                        await CalcularDescontoAsync(carrinho);
                        HttpContext.Session.Set(SessaoCarrinho, carrinho);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AlterarTamanho(int produtoIdAtual, int novoProdutoId)
        {
            if (produtoIdAtual == novoProdutoId)
            {
                return RedirectToAction("Index", "Checkout");
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var itemAtual = await _context.CarrinhoItens
                    .FirstOrDefaultAsync(c => c.UsuarioId == user.Id && c.ProdutoId == produtoIdAtual);

                if (itemAtual != null)
                {
                    var itemMesmoProduto = await _context.CarrinhoItens
                        .FirstOrDefaultAsync(c => c.UsuarioId == user.Id && c.ProdutoId == novoProdutoId);

                    if (itemMesmoProduto != null)
                    {
                        itemMesmoProduto.Quantidade += itemAtual.Quantidade;
                        _context.CarrinhoItens.Remove(itemAtual);
                    }
                    else
                    {
                        itemAtual.ProdutoId = novoProdutoId;
                        _context.CarrinhoItens.Update(itemAtual);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var carrinho = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
                if (carrinho != null)
                {
                    var itemAtual = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoIdAtual);
                    if (itemAtual != null)
                    {
                        var produtoNovo = await _context.Produtos
                            .Include(p => p.Tamanho)
                            .FirstOrDefaultAsync(p => p.Id == novoProdutoId);

                        if (produtoNovo != null)
                        {
                            var itemMesmoProduto = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == novoProdutoId);
                            if (itemMesmoProduto != null)
                            {
                                itemMesmoProduto.Quantidade += itemAtual.Quantidade;
                                carrinho.Itens.Remove(itemAtual);
                            }
                            else
                            {
                                itemAtual.ProdutoId = produtoNovo.Id;
                                itemAtual.Nome = produtoNovo.Nome;
                                itemAtual.Preco = produtoNovo.Preco;
                                itemAtual.ImagemUrl = produtoNovo.ImagemUrl;
                                itemAtual.ValorFreteFixo = produtoNovo.ValorFreteFixo;
                                itemAtual.Tamanho = produtoNovo.Tamanho?.Nome;
                            }

                            carrinho.ValorFrete = 0;
                            await CalcularDescontoAsync(carrinho);
                            HttpContext.Session.Set(SessaoCarrinho, carrinho);
                        }
                    }
                }
            }

            TempData["MensagemSucesso"] = "Tamanho atualizado com sucesso!";
            return RedirectToAction("Index", "Checkout");
        }

        [HttpPost]
        public async Task<IActionResult> AplicarCupom(string codigoCupom)
        {
            // Nota: Cupons ainda são aplicados na Session/ViewModel temporariamente,
            // pois não criamos campo de Cupom na tabela Carrinho (apenas CarrinhoItem).
            // Para simplificar, vou manter a lógica de cupom na Sessão ou persistir no ViewModel retornado.
            // O ideal seria ter uma tabela Carrinho header, mas vou adaptar.
            
            // Para manter persistência do cupom entre requisições para user logado, precisaríamos salvar no banco.
            // Como solução paliativa rápida: salvar cupom na sessão mesmo para user logado, já que é algo da "sessão de compra".
            
            var carrinho = await ObterCarrinhoAtualizadoAsync();

            if (string.IsNullOrWhiteSpace(codigoCupom))
            {
                carrinho.CupomAplicado = null;
                carrinho.ValorDesconto = 0;
                carrinho.MensagemCupom = "Cupom removido.";
            }
            else
            {
                carrinho.CupomAplicado = codigoCupom;
                await CalcularDescontoAsync(carrinho);
            }

            // Se logado, não temos onde salvar o cupom no banco (tabela CarrinhoItem não tem header).
            // Então salvamos na sessão para persistir durante a navegação.
            HttpContext.Session.Set(SessaoCarrinho, carrinho);
            
            return RedirectToAction(nameof(Index));
        }

        private async Task CalcularDescontoAsync(CarrinhoViewModel carrinho)
        {
            if (string.IsNullOrEmpty(carrinho.CupomAplicado))
            {
                carrinho.ValorDesconto = 0;
                carrinho.MensagemCupom = null;
                return;
            }

            var cupom = await _context.Cupons
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Codigo == carrinho.CupomAplicado && c.Ativo);

            if (cupom == null)
            {
                carrinho.MensagemCupom = "Cupom inválido ou expirado.";
                carrinho.ValorDesconto = 0;
            }
            else
            {
                var produtosComCupom = await _context.Produtos
                    .AsNoTracking()
                    .Where(p => p.CupomId == cupom.Id)
                    .Select(p => p.Id)
                    .ToListAsync();

                decimal descontoTotal = 0;
                bool cupomAplicavel = false;

                foreach (var item in carrinho.Itens)
                {
                    if (produtosComCupom.Contains(item.ProdutoId))
                    {
                        descontoTotal += item.Total * (cupom.PercentualDesconto / 100m);
                        cupomAplicavel = true;
                    }
                }

                if (cupomAplicavel)
                {
                    carrinho.ValorDesconto = descontoTotal;
                    carrinho.MensagemCupom = $"Cupom aplicado! -{cupom.PercentualDesconto}%";
                }
                else
                {
                    carrinho.MensagemCupom = "Este cupom não se aplica aos itens do carrinho.";
                    carrinho.ValorDesconto = 0;
                }
            }
        }

        private async Task<CarrinhoViewModel> ObterCarrinhoAtualizadoAsync()
        {
            CarrinhoViewModel carrinho;
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Carrega do Banco
                var itensBanco = await _context.CarrinhoItens
                    .Include(c => c.Produto)
                    .ThenInclude(p => p.Tamanho)
                    .Where(c => c.UsuarioId == user.Id)
                    .ToListAsync();

                carrinho = new CarrinhoViewModel();
                foreach (var ib in itensBanco)
                {
                    carrinho.Itens.Add(new ItemCarrinhoViewModel
                    {
                        ProdutoId = ib.ProdutoId,
                        Nome = ib.Produto.Nome,
                        Preco = ib.Produto.Preco,
                        Quantidade = ib.Quantidade,
                        ImagemUrl = ib.Produto.ImagemUrl,
                        ValorFreteFixo = ib.Produto.ValorFreteFixo,
                        Tamanho = ib.Produto.Tamanho?.Nome
                    });
                }
                
                // Recupera Cupom/Frete da sessão se houver (já que não temos tabela CarrinhoHeader)
                var sessao = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
                if (sessao != null)
                {
                    carrinho.CupomAplicado = sessao.CupomAplicado;
                    carrinho.ValorFrete = sessao.ValorFrete;
                    carrinho.TipoFrete = sessao.TipoFrete;
                    carrinho.PrazoFreteDias = sessao.PrazoFreteDias;
                }
            }
            else
            {
                // Carrega da Sessão
                carrinho = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho) ?? new CarrinhoViewModel();
            }

            // Recalcula descontos se tiver cupom
            if (!string.IsNullOrEmpty(carrinho.CupomAplicado))
            {
                await CalcularDescontoAsync(carrinho);
            }

            return carrinho;
        }
        
        private async Task SalvarCarrinhoAsync(CarrinhoViewModel carrinho)
        {
             // Método auxiliar apenas para salvar metadados na sessão (frete, cupom),
             // pois itens de logado já são salvos diretamente no banco.
             HttpContext.Session.Set(SessaoCarrinho, carrinho);
        }
    }
}
