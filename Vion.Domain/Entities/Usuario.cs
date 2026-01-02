using System.ComponentModel.DataAnnotations.Schema;

namespace Vion.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;

    // 🔑 FK
    public int TipoUsuarioId { get; set; }

    // 🔗 Navegação
    public TipoUsuario TipoUsuario { get; set; } = null!;
}
