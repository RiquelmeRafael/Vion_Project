namespace Vion.Domain.Entities
{
    public class HomeFeaturedItem
    {
        public int Id { get; set; }
        public string Tag { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string PillText { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public int? CategoriaId { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

