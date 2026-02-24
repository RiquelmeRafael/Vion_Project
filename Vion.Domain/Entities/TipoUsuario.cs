using System.Collections.Generic;

namespace Vion.Domain.Entities
{
    public class TipoUsuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";

        // 🔥 ISSO ESTAVA FALTANDO
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
