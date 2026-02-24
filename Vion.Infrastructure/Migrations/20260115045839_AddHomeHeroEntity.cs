using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeHeroEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeHeros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eyebrow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitlePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleHighlight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleSuffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainButtonCategoriaId = table.Column<int>(type: "int", nullable: true),
                    SecondaryButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardPriceCurrent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardPriceOld = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CardSizeText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardBadgeText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardCategoriaId = table.Column<int>(type: "int", nullable: true),
                    CardImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccentColorHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeHeros", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeHeros");
        }
    }
}
