using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vion.Domain.Entities;
using Vion.Infrastructure.Persistence;

namespace Vion.Web.Controllers
{
    [Authorize]
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
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var conversation = await _context.ChatConversations
                .Include(c => c.Mensagens)
                .ThenInclude(m => m.Remetente)
                .FirstOrDefaultAsync(c => c.ClienteId == user.Id && !c.Encerrado);

            if (conversation == null)
            {
                conversation = new ChatConversation
                {
                    ClienteId = user.Id
                };
                _context.ChatConversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            // Garante que a lista de mensagens não seja nula para a View
            if (conversation.Mensagens == null)
            {
                conversation.Mensagens = new List<ChatMessage>();
            }

            return View(conversation);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarMensagem(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Busca conversa ativa
            var conversation = await _context.ChatConversations
                .FirstOrDefaultAsync(c => c.ClienteId == user.Id && !c.Encerrado);

            if (conversation == null)
            {
                // Se não existir (estranho, mas possível), cria
                conversation = new ChatConversation
                {
                    ClienteId = user.Id
                };
                _context.ChatConversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            var mensagem = new ChatMessage
            {
                ConversationId = conversation.Id,
                RemetenteId = user.Id,
                Conteudo = conteudo.Trim(),
                RemetenteEhStaff = false,
                EnviadoEm = DateTime.Now
            };

            _context.ChatMessages.Add(mensagem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EnviarMensagemAjax(int conversationId, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var conversation = await _context.ChatConversations
                .FirstOrDefaultAsync(c => c.Id == conversationId && c.ClienteId == user.Id && !c.Encerrado);

            if (conversation == null)
            {
                conversation = new ChatConversation
                {
                    ClienteId = user.Id
                };
                _context.ChatConversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            var mensagem = new ChatMessage
            {
                ConversationId = conversation.Id,
                RemetenteId = user.Id,
                Conteudo = conteudo.Trim(),
                RemetenteEhStaff = false,
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

        [HttpGet]
        public async Task<IActionResult> ObterMensagens(int conversationId, int lastMessageId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var conversation = await _context.ChatConversations
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == conversationId && c.ClienteId == user.Id && !c.Encerrado);

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
