using Microsoft.AspNetCore.Identity;

namespace Vion.Domain.Entities
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome { get; set; } = "";

        public int TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; } = null!;
    }
}
