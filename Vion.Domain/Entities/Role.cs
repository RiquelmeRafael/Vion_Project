namespace Vion.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
