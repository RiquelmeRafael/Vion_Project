using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Gerente")]
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ChatController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var conversas = await _context.ChatConversations
                .Include(c => c.Cliente)
                .Include(c => c.Mensagens)
                .Where(c => c.Mensagens.Any())
                .OrderByDescending(c => c.CriadoEm)
                .ToListAsync();

            return View(conversas);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var conversation = await _context.ChatConversations
                .Include(c => c.Cliente)
                .Include(c => c.Mensagens)
                .ThenInclude(m => m.Remetente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conversation == null) return NotFound();

            ViewBag.ConversationId = conversation.Id;
            return View(conversation);
        }

        [HttpPost]
        public async Task<IActionResult> Responder(int conversationId, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return RedirectToAction(nameof(Detalhes), new { id = conversationId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var conversation = await _context.ChatConversations.FirstOrDefaultAsync(c => c.Id == conversationId);
            if (conversation == null) return RedirectToAction(nameof(Index));

            conversation.AtendenteId = user.Id;

            var mensagem = new ChatMessage
            {
                ConversationId = conversation.Id,
                RemetenteId = user.Id,
                Conteudo = conteudo.Trim(),
                RemetenteEhStaff = true,
                EnviadoEm = DateTime.Now
            };

            _context.ChatMessages.Add(mensagem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detalhes), new { id = conversationId });
        }

        [HttpPost]
        public async Task<IActionResult> ResponderAjax(int conversationId, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var conversation = await _context.ChatConversations.FirstOrDefaultAsync(c => c.Id == conversationId);
            if (conversation == null) return NotFound();

            conversation.AtendenteId = user.Id;

            var mensagem = new ChatMessage
            {
                ConversationId = conversation.Id,
                RemetenteId = user.Id,
                Conteudo = conteudo.Trim(),
                RemetenteEhStaff = true,
                EnviadoEm = DateTime.Now
            };

            _context.ChatMessages.Add(mensagem);
            await _context.SaveChangesAsync();

            return Json(new
            {
                id = mensagem.Id,
                conteudo = mensagem.Conteudo,
                remetenteEhStaff = mensagem.RemetenteEhStaff,
                hora = mensagem.EnviadoEm.ToLocalTime().ToString("HH:mm")
            });
        }

        [HttpPost]
        public async Task<IActionResult> Encerrar(int id)
        {
            var conversation = await _context.ChatConversations.FindAsync(id);
            if (conversation == null) return NotFound();

            conversation.Encerrado = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ObterMensagens(int conversationId, int lastMessageId)
        {
            var conversation = await _context.ChatConversations
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == conversationId);

            if (conversation == null) return NotFound();

            var mensagens = await _context.ChatMessages
                .AsNoTracking()
                .Where(m => m.ConversationId == conversationId && m.Id > lastMessageId)
                .OrderBy(m => m.EnviadoEm)
                .Select(m => new
                {
                    id = m.Id,
                    conteudo = m.Conteudo,
                    remetenteEhStaff = m.RemetenteEhStaff,
                    hora = m.EnviadoEm.ToLocalTime().ToString("HH:mm")
                })
                .ToListAsync();

            return Json(new { mensagens });
        }
    }
}
