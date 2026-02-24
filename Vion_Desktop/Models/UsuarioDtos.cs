namespace Vion_Desktop.Models
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TipoUsuarioId { get; set; }
        public string TipoUsuario { get; set; } = string.Empty;
    }

    public class UsuarioCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int TipoUsuarioId { get; set; }
    }

    public class UsuarioUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Senha { get; set; }
        public int TipoUsuarioId { get; set; }
    }

    public class TipoUsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
