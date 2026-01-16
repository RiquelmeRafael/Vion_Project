using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class ManageController : Controller
    {
        private readonly AppDbContext _context;

        public ManageController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditHomeHero()
        {
            var hero = await _context.HomeHeros
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.IsActive);

            if (hero == null)
            {
                hero = new HomeHero
                {
                    Eyebrow = "Lançamento exclusivo",
                    TitlePrefix = "O próximo passo do",
                    TitleHighlight = "street sport",
                    TitleSuffix = "chegou.",
                    Subtitle = "Silhuetas agressivas, amortecimento responsivo e design pensado para quem vive o movimento 24/7. VION é o novo ritmo da sua cidade.",
                    MainButtonText = "Comprar agora",
                    SecondaryButtonText = "Ver coleções",
                    CardTag = "VION AIR UNIT",
                    CardTitle = "VION X STREET EDITION",
                    CardPriceCurrent = 499.90m,
                    CardPriceOld = 699.90m,
                    CardSizeText = "Do 37 ao 44",
                    CardBadgeText = "Drop limitado",
                    AccentColorHex = "#4cffb3",
                    IsActive = true
                };
            }

            await PopularCategoriasAsync();
            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHomeHero(HomeHero model)
        {
            if (!ModelState.IsValid)
            {
                await PopularCategoriasAsync();
                return View(model);
            }

            HomeHero? existing = null;

            if (model.Id > 0)
            {
                existing = await _context.HomeHeros.FirstOrDefaultAsync(h => h.Id == model.Id);
            }

            if (existing == null)
            {
                model.IsActive = true;
                _context.HomeHeros.Add(model);
            }
            else
            {
                existing.Eyebrow = model.Eyebrow;
                existing.TitlePrefix = model.TitlePrefix;
                existing.TitleHighlight = model.TitleHighlight;
                existing.TitleSuffix = model.TitleSuffix;
                existing.Subtitle = model.Subtitle;
                existing.MainButtonText = model.MainButtonText;
                existing.MainButtonCategoriaId = model.MainButtonCategoriaId;
                existing.SecondaryButtonText = model.SecondaryButtonText;
                existing.CardTag = model.CardTag;
                existing.CardTitle = model.CardTitle;
                existing.CardPriceCurrent = model.CardPriceCurrent;
                existing.CardPriceOld = model.CardPriceOld;
                existing.CardSizeText = model.CardSizeText;
                existing.CardBadgeText = model.CardBadgeText;
                existing.CardCategoriaId = model.CardCategoriaId;
                existing.CardImageUrl = model.CardImageUrl;
                existing.AccentColorHex = model.AccentColorHex;
                existing.IsActive = true;

                _context.HomeHeros.Update(existing);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Hero da Home atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditHomeFeatured()
        {
            var itens = await _context.HomeFeaturedItems
                .AsNoTracking()
                .OrderBy(i => i.Position)
                .ToListAsync();

            if (!itens.Any())
            {
                var tenisCategoria = await _context.Categorias
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Nome == "Tênis");

                var tenisId = tenisCategoria?.Id;

                itens = new List<HomeFeaturedItem>
                {
                    new HomeFeaturedItem
                    {
                        Tag = "Performance urbana",
                        Name = "VION Flux Runner",
                        Price = 429.90m,
                        PillText = "Lançamento",
                        ImageUrl = "/images/products/9168ba84-e88b-4ad9-ad34-6e3e0d30f2b0_tenis3.webp",
                        CategoriaId = tenisId,
                        Position = 1,
                        IsActive = true
                    },
                    new HomeFeaturedItem
                    {
                        Tag = "Dia a dia",
                        Name = "VION Pulse Essential",
                        Price = 379.90m,
                        PillText = "Best seller",
                        ImageUrl = "/images/products/6e5069cb-ba17-490d-b704-8bef9336a9fb_tenis4.webp",
                        CategoriaId = tenisId,
                        Position = 2,
                        IsActive = true
                    },
                    new HomeFeaturedItem
                    {
                        Tag = "Night mode",
                        Name = "VION Neon Street",
                        Price = 519.90m,
                        PillText = "Glow details",
                        ImageUrl = "/images/products/f948b180-1110-4234-833c-9728d879d91e_tenis4.webp",
                        CategoriaId = tenisId,
                        Position = 3,
                        IsActive = true
                    }
                };
            }

            await PopularCategoriasAsync();
            return View(itens);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHomeFeatured(List<HomeFeaturedItem> model)
        {
            if (!ModelState.IsValid)
            {
                await PopularCategoriasAsync();
                return View(model);
            }

            var existentes = await _context.HomeFeaturedItems
                .ToListAsync();

            foreach (var item in model.OrderBy(i => i.Position))
            {
                HomeFeaturedItem? entity = null;

                if (item.Id > 0)
                {
                    entity = existentes.FirstOrDefault(e => e.Id == item.Id);
                }

                if (entity == null)
                {
                    item.IsActive = true;
                    _context.HomeFeaturedItems.Add(item);
                }
                else
                {
                    entity.Tag = item.Tag;
                    entity.Name = item.Name;
                    entity.Price = item.Price;
                    entity.PillText = item.PillText;
                    entity.ImageUrl = item.ImageUrl;
                    entity.CategoriaId = item.CategoriaId;
                    entity.Position = item.Position;
                    entity.IsActive = item.IsActive;

                    _context.HomeFeaturedItems.Update(entity);
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Destaques da semana atualizados com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopularCategoriasAsync()
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
        }
    }
}
