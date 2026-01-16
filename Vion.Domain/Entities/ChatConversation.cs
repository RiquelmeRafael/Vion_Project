namespace Vion.Domain.Entities
{
    public class ChatConversation
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; } = null!;
        public int? AtendenteId { get; set; }
        public Usuario? Atendente { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public bool Encerrado { get; set; }
        public ICollection<ChatMessage> Mensagens { get; set; } = new List<ChatMessage>();
    }
}

