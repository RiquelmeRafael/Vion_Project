namespace Vion_Desktop.Models
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class CategoriaCreateDto
    {
        public string Nome { get; set; } = string.Empty;
    }

    public class CategoriaUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
    }
}
