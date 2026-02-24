using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProdutosController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<string?> UploadImage(IFormFile? arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + arquivo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await arquivo.CopyToAsync(fileStream);
                }

                return "/images/products/" + uniqueFileName;
            }
            return null;
        }

        // GET: Admin/Produtos
        public async Task<IActionResult> Index(int? categoriaId)
        {
            var appDbContext = _context.Produtos.Include(p => p.Categoria).Include(p => p.Tamanho).AsQueryable();

            if (categoriaId.HasValue)
            {
                appDbContext = appDbContext.Where(p => p.CategoriaId == categoriaId);
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", categoriaId);

            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Tamanho)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Admin/Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["TamanhoId"] = new SelectList(_context.Tamanhos, "Id", "Nome");
            ViewData["CupomId"] = new SelectList(_context.Cupons.Where(c => c.Ativo), "Id", "Codigo");
            return View();
        }

        // POST: Admin/Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nome,Descricao,Preco,CategoriaId,TamanhoId,Cor,Estoque,ImagemUrl,ImagemUrl2,ImagemUrl3,ImagemUrl4,ValorFreteFixo,CupomId")] Produto produto, 
            int[] TamanhosSelecionados, 
            Dictionary<int, int> EstoquePorTamanho,
            IFormFile? img1, IFormFile? img2, IFormFile? img3, IFormFile? img4)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Categoria");
            ModelState.Remove("Tamanho");
            ModelState.Remove("Cupom");

            // Remover validação de TamanhoId e Estoque se estiver usando seleção múltipla
            if (TamanhosSelecionados != null && TamanhosSelecionados.Length > 0)
            {
                ModelState.Remove("TamanhoId");
                ModelState.Remove("Estoque");
            }

            // Upload de Imagens
            var path1 = await UploadImage(img1);
            if (path1 != null) 
            {
                produto.ImagemUrl = path1;
                ModelState.Remove("ImagemUrl");
            }

            var path2 = await UploadImage(img2);
            if (path2 != null) produto.ImagemUrl2 = path2;

            var path3 = await UploadImage(img3);
            if (path3 != null) produto.ImagemUrl3 = path3;

            var path4 = await UploadImage(img4);
            if (path4 != null) produto.ImagemUrl4 = path4;

            if (ModelState.IsValid)
            {
                produto.CreatedAt = DateTime.Now;

                if (TamanhosSelecionados != null && TamanhosSelecionados.Length > 0)
                {
                    // Criação Múltipla (Vários tamanhos)
                    foreach (var tamId in TamanhosSelecionados)
                    {
                        var novoProd = new Produto
                        {
                            Nome = produto.Nome,
                            Descricao = produto.Descricao,
                            Preco = produto.Preco,
                            ValorFreteFixo = produto.ValorFreteFixo,
                            CategoriaId = produto.CategoriaId,
                            Cor = produto.Cor,
                            ImagemUrl = produto.ImagemUrl,
                            ImagemUrl2 = produto.ImagemUrl2,
                            ImagemUrl3 = produto.ImagemUrl3,
                            ImagemUrl4 = produto.ImagemUrl4,
                            CupomId = produto.CupomId,
                            TamanhoId = tamId,
                            Estoque = EstoquePorTamanho.ContainsKey(tamId) ? EstoquePorTamanho[tamId] : 0,
                            CreatedAt = produto.CreatedAt
                        };
                        _context.Add(novoProd);
                    }
                }
                else
                {
                    _context.Add(produto);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
            ViewData["TamanhoId"] = new SelectList(_context.Tamanhos, "Id", "Nome", produto.TamanhoId);
            ViewData["CupomId"] = new SelectList(_context.Cupons.Where(c => c.Ativo), "Id", "Codigo", produto.CupomId);
            return View(produto);
        }

        // GET: Admin/Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            // Busca variantes do mesmo grupo (mesmo Nome e Cor)
            var variants = await _context.Produtos
                .Where(p => p.Nome == produto.Nome && p.Cor == produto.Cor && p.CategoriaId == produto.CategoriaId)
                .ToListAsync();

            // Mapeia TamanhoId -> Estoque para a View preencher a tabela
            ViewData["ExistingVariants"] = variants.ToDictionary(p => p.TamanhoId, p => p.Estoque);

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
            ViewData["TamanhoId"] = new SelectList(_context.Tamanhos, "Id", "Nome", produto.TamanhoId);
            ViewData["CupomId"] = new SelectList(_context.Cupons.Where(c => c.Ativo), "Id", "Codigo", produto.CupomId);
            return View(produto);
        }

        // POST: Admin/Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("Id,Nome,Descricao,Preco,CategoriaId,TamanhoId,Cor,Estoque,ImagemUrl,ImagemUrl2,ImagemUrl3,ImagemUrl4,CreatedAt,ValorFreteFixo,CupomId")] Produto produto, 
            int[] TamanhosSelecionados, 
            Dictionary<int, int> EstoquePorTamanho,
            IFormFile? img1, IFormFile? img2, IFormFile? img3, IFormFile? img4)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            // Remove navigation properties from validation
            ModelState.Remove("Categoria");
            ModelState.Remove("Tamanho");
            ModelState.Remove("Cupom");

            // Remover validação de TamanhoId e Estoque se estiver usando seleção múltipla
            if (TamanhosSelecionados != null && TamanhosSelecionados.Length > 0)
            {
                ModelState.Remove("TamanhoId");
                ModelState.Remove("Estoque");
            }

            // Upload de Imagens (Se enviadas, substitui as URLs existentes)
            var path1 = await UploadImage(img1);
            if (path1 != null) 
            {
                produto.ImagemUrl = path1;
                ModelState.Remove("ImagemUrl");
            }

            var path2 = await UploadImage(img2);
            if (path2 != null) produto.ImagemUrl2 = path2;

            var path3 = await UploadImage(img3);
            if (path3 != null) produto.ImagemUrl3 = path3;

            var path4 = await UploadImage(img4);
            if (path4 != null) produto.ImagemUrl4 = path4;

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Busca o produto original para identificar o grupo (antes da edição)
                    var originalProduto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (originalProduto == null) return NotFound();

                    // 2. Busca todas as variantes do grupo original
                    var siblings = await _context.Produtos
                        .Where(p => p.Nome == originalProduto.Nome && p.Cor == originalProduto.Cor && p.CategoriaId == originalProduto.CategoriaId)
                        .ToListAsync();

                    // 3. Atualiza ou Cria Variantes
                    if (TamanhosSelecionados != null && TamanhosSelecionados.Length > 0)
                    {
                        foreach (var tamId in TamanhosSelecionados)
                        {
                            var existingVariant = siblings.FirstOrDefault(p => p.TamanhoId == tamId);
                            var estoque = EstoquePorTamanho.ContainsKey(tamId) ? EstoquePorTamanho[tamId] : 0;

                            if (existingVariant != null)
                            {
                                // Atualiza variante existente
                                existingVariant.Nome = produto.Nome;
                                existingVariant.Descricao = produto.Descricao;
                                existingVariant.Preco = produto.Preco;
                                existingVariant.ValorFreteFixo = produto.ValorFreteFixo;
                                existingVariant.CategoriaId = produto.CategoriaId;
                                existingVariant.Cor = produto.Cor;
                                existingVariant.ImagemUrl = produto.ImagemUrl;
                                existingVariant.ImagemUrl2 = produto.ImagemUrl2;
                                existingVariant.ImagemUrl3 = produto.ImagemUrl3;
                                existingVariant.ImagemUrl4 = produto.ImagemUrl4;
                                existingVariant.CupomId = produto.CupomId;
                                existingVariant.Estoque = estoque;
                                
                                _context.Update(existingVariant);
                                // Remove da lista de siblings para saber quem sobrou (quem foi desmarcado)
                                siblings.Remove(existingVariant);
                            }
                            else
                            {
                                // Cria nova variante
                                var newVariant = new Produto
                                {
                                    Nome = produto.Nome,
                                    Descricao = produto.Descricao,
                                    Preco = produto.Preco,
                                    ValorFreteFixo = produto.ValorFreteFixo,
                                    CategoriaId = produto.CategoriaId,
                                    Cor = produto.Cor,
                                    ImagemUrl = produto.ImagemUrl,
                                    ImagemUrl2 = produto.ImagemUrl2,
                                    ImagemUrl3 = produto.ImagemUrl3,
                                    ImagemUrl4 = produto.ImagemUrl4,
                                    CupomId = produto.CupomId,
                                    TamanhoId = tamId,
                                    Estoque = estoque,
                                    CreatedAt = originalProduto.CreatedAt
                                };
                                _context.Add(newVariant);
                            }
                        }

                        // 4. Remove variantes que foram desmarcadas
                        if (siblings.Any())
                        {
                            _context.Produtos.RemoveRange(siblings);
                        }
                    }
                    else
                    {
                        // Se nada selecionado, fallback para edição simples (apenas o produto atual)
                        // Mas idealmente o form obriga seleção. Vamos manter o update simples caso o array venha vazio (ex: erro JS)
                        _context.Update(produto);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            // Recarrega dados em caso de erro
            var variants = await _context.Produtos
                .Where(p => p.Nome == produto.Nome && p.Cor == produto.Cor && p.CategoriaId == produto.CategoriaId)
                .ToListAsync();
            ViewData["ExistingVariants"] = variants.ToDictionary(p => p.TamanhoId, p => p.Estoque);

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
            ViewData["TamanhoId"] = new SelectList(_context.Tamanhos, "Id", "Nome", produto.TamanhoId);
            ViewData["CupomId"] = new SelectList(_context.Cupons.Where(c => c.Ativo), "Id", "Codigo", produto.CupomId);
            return View(produto);
        }

        // GET: Admin/Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Tamanho)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Admin/Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
