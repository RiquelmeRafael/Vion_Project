using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Vion.Web.Extensions;
using Vion.Web.Models.ViewModels;
using Vion.Web.Services;

namespace Vion.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IEmailService _emailService;
        private const string SessaoCarrinho = "Carrinho";

        public CheckoutController(AppDbContext context, UserManager<Usuario> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carrinho = await ObterCarrinhoAtualizadoAsync();

            if (carrinho == null || !carrinho.Itens.Any())
            {
                TempData["MensagemErro"] = "Seu carrinho está vazio.";
                return RedirectToAction("Index", "Carrinho");
            }

            var user = await _userManager.GetUserAsync(User);
            
            var viewModel = new CheckoutViewModel
            {
                Carrinho = carrinho,
                NomeCompleto = user?.Nome,
                Email = user?.Email,
                // Poderíamos preencher endereço se o User tivesse esses campos
            };

            var tamanhosPorProduto = await MontarTamanhosPorProdutoAsync(carrinho);
            ViewBag.TamanhosPorProduto = tamanhosPorProduto;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Processar(CheckoutViewModel model)
        {
            var carrinho = await ObterCarrinhoAtualizadoAsync();

            if (carrinho == null || !carrinho.Itens.Any())
            {
                return RedirectToAction("Index", "Carrinho");
            }

            if (!ModelState.IsValid)
            {
                model.Carrinho = carrinho;
                return View("Index", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Criar Pedido
            var pedido = new Pedido
            {
                UsuarioId = user.Id,
                DataPedido = DateTime.UtcNow,
                Status = "Pendente",
                FormaPagamento = model.FormaPagamento!,
                ValorTotal = carrinho.Total,
                ValorFrete = carrinho.TotalFrete,
                
                // Dados do Cliente e Endereço
                Cpf = model.Cpf,
                NomeCliente = model.NomeCompleto,
                EmailCliente = model.Email,
                Cep = model.Cep,
                Endereco = model.Endereco,
                Cidade = model.Cidade,
                Estado = model.Estado
            };

            // Processar Cupom
            if (!string.IsNullOrEmpty(carrinho.CupomAplicado))
            {
                var cupom = await _context.Cupons.AsNoTracking().FirstOrDefaultAsync(c => c.Codigo == carrinho.CupomAplicado);
                if (cupom != null)
                {
                    pedido.CupomId = cupom.Id;
                    pedido.ValorDesconto = carrinho.ValorDesconto;
                }
            }
            
            // Valor Total final (incluindo frete e desconto)
            pedido.ValorTotal = carrinho.TotalComFrete;

            // Adicionar Itens
            foreach (var item in carrinho.Itens)
            {
                pedido.Itens.Add(new ItemPedido
                {
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.Preco
                });
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // Recarregar pedido com produtos para o e-mail
            var pedidoCompleto = await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == pedido.Id);

            if (pedidoCompleto != null)
            {
                // Enviar e-mail de confirmação
                await _emailService.SendOrderConfirmationEmailAsync(pedidoCompleto);
            }

            // Limpar Carrinho do Banco e Sessão
            var itensRemover = await _context.CarrinhoItens.Where(c => c.UsuarioId == user.Id).ToListAsync();
            if (itensRemover.Any())
            {
                _context.CarrinhoItens.RemoveRange(itensRemover);
                await _context.SaveChangesAsync();
            }
            HttpContext.Session.Remove(SessaoCarrinho);

            return RedirectToAction(nameof(Sucesso), new { id = pedido.Id });
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
                    carrinho.MensagemCupom = $"Cupom {cupom.Codigo} aplicado: {cupom.PercentualDesconto}% de desconto em itens selecionados.";
                }
                else
                {
                    carrinho.MensagemCupom = "Este cupom não se aplica aos itens do carrinho.";
                    carrinho.ValorDesconto = 0;
                }
            }
        }

        private async Task<CarrinhoViewModel?> ObterCarrinhoAtualizadoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return null;

            // 1. Carregar Itens do Banco
            var itensBanco = await _context.CarrinhoItens
                .Include(c => c.Produto)
                .ThenInclude(p => p.Tamanho)
                .Where(c => c.UsuarioId == user.Id)
                .ToListAsync();

            if (!itensBanco.Any()) return null;

            var carrinho = new CarrinhoViewModel();
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

            // 2. Recuperar Metadados da Sessão (Cupom, Frete)
            var sessao = HttpContext.Session.Get<CarrinhoViewModel>(SessaoCarrinho);
            if (sessao != null)
            {
                carrinho.CupomAplicado = sessao.CupomAplicado;
                carrinho.ValorFrete = sessao.ValorFrete;
                carrinho.TipoFrete = sessao.TipoFrete;
                carrinho.PrazoFreteDias = sessao.PrazoFreteDias;
            }

            // 3. Recalcular Frete Fixo se necessário (para garantir consistência)
            if (carrinho.Itens.Any(i => !i.ValorFreteFixo.HasValue))
            {
                var ids = carrinho.Itens.Select(i => i.ProdutoId).Distinct().ToList();
                var fretes = await _context.Produtos
                    .Where(p => ids.Contains(p.Id))
                    .Select(p => new { p.Id, p.ValorFreteFixo })
                    .ToListAsync();

                var map = fretes.ToDictionary(x => x.Id, x => x.ValorFreteFixo);
                foreach (var item in carrinho.Itens)
                {
                    if (!item.ValorFreteFixo.HasValue && map.TryGetValue(item.ProdutoId, out var freteFixo))
                    {
                        item.ValorFreteFixo = freteFixo;
                    }
                }
            }

            // 4. Recalcular Descontos
            if (!string.IsNullOrEmpty(carrinho.CupomAplicado))
            {
                await CalcularDescontoAsync(carrinho);
            }

            return carrinho;
        }

        private async Task<Dictionary<int, List<SelectListItem>>> MontarTamanhosPorProdutoAsync(CarrinhoViewModel carrinho)
        {
            var resultado = new Dictionary<int, List<SelectListItem>>();

            var idsProdutosCarrinho = carrinho.Itens
                .Select(i => i.ProdutoId)
                .Distinct()
                .ToList();

            if (!idsProdutosCarrinho.Any())
                return resultado;

            var produtosCarrinho = await _context.Produtos
                .Where(p => idsProdutosCarrinho.Contains(p.Id))
                .ToListAsync();

            foreach (var produto in produtosCarrinho)
            {
                var variantes = await _context.Produtos
                    .Include(v => v.Tamanho)
                    .Where(v => v.Nome == produto.Nome &&
                                v.Cor == produto.Cor &&
                                v.CategoriaId == produto.CategoriaId &&
                                v.Estoque > 0)
                    .OrderBy(v => v.Tamanho.Nome)
                    .ToListAsync();

                var lista = variantes
                    .Select(v => new SelectListItem
                    {
                        Value = v.Id.ToString(),
                        Text = v.Tamanho.Nome,
                        Selected = v.Id == produto.Id
                    })
                    .ToList();

                resultado[produto.Id] = lista;
            }

            return resultado;
        }

        public IActionResult Sucesso(int id)
        {
            return View(id);
        }

        [HttpGet]
        public async Task<IActionResult> MeusPedidos()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var pedidos = await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.UsuarioId == user.Id)
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();

            var avaliacoes = await _context.Avaliacoes
                .AsNoTracking()
                .Where(a => a.UsuarioId == user.Id)
                .Select(a => new { a.PedidoId, a.ProdutoId })
                .ToListAsync();

            ViewBag.AvaliacoesFeitas = avaliacoes
                .Select(a => $"{a.PedidoId}-{a.ProdutoId}")
                .ToHashSet();

            return View(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id && p.UsuarioId == user.Id);
            
            if (pedido == null) return NotFound();
            
            if (pedido.Status == "Pendente")
            {
                pedido.Status = "Cancelado";
                _context.Update(pedido);
                await _context.SaveChangesAsync();
                TempData["Mensagem"] = "Pedido cancelado com sucesso.";
            }
            else
            {
                TempData["Erro"] = "Não é possível cancelar este pedido pois ele já foi processado.";
            }

            return RedirectToAction(nameof(MeusPedidos));
        }
    }
}
