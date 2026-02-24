namespace Vion_Desktop.Models
{
    public class TamanhoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class TamanhoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
    }

    public class TamanhoUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
    }
}
