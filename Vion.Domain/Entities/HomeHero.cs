namespace Vion.Domain.Entities
{
    public class HomeHero
    {
        public int Id { get; set; }

        public string Eyebrow { get; set; } = "";
        public string TitlePrefix { get; set; } = "";
        public string TitleHighlight { get; set; } = "";
        public string TitleSuffix { get; set; } = "";
        public string Subtitle { get; set; } = "";

        public string MainButtonText { get; set; } = "";
        public int? MainButtonCategoriaId { get; set; }

        public string SecondaryButtonText { get; set; } = "";

        public string CardTag { get; set; } = "";
        public string CardTitle { get; set; } = "";
        public decimal CardPriceCurrent { get; set; }
        public decimal? CardPriceOld { get; set; }
        public string CardSizeText { get; set; } = "";
        public string CardBadgeText { get; set; } = "";
        public int? CardCategoriaId { get; set; }

        public string CardImageUrl { get; set; } = "";

        public string AccentColorHex { get; set; } = "#4cffb3";

        public bool IsActive { get; set; } = true;
    }
}

