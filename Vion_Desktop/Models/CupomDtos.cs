namespace Vion_Desktop.Models
{
    public class CupomDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public decimal PercentualDesconto { get; set; }
        public bool Ativo { get; set; }
    }

    public class CupomCreateDto
    {
        public string Codigo { get; set; } = string.Empty;
        public decimal PercentualDesconto { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
