namespace Vion.Domain.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public ChatConversation Conversation { get; set; } = null!;
        public int RemetenteId { get; set; }
        public Usuario Remetente { get; set; } = null!;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime EnviadoEm { get; set; } = DateTime.UtcNow;
        public bool Lido { get; set; }
        public bool RemetenteEhStaff { get; set; }
    }
}

