using System;

namespace Vion.Domain.Entities
{
    // Entidade que representa um produto favoritado por um usuário
    public class Favorito
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        public DateTime DataAdicionado { get; set; } = DateTime.Now;
    }
}
