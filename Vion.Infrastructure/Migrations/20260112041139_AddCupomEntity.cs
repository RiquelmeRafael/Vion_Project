using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCupomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CupomId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PercentualDesconto = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CupomId",
                table: "Produtos",
                column: "CupomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Cupons_CupomId",
                table: "Produtos",
                column: "CupomId",
                principalTable: "Cupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Cupons_CupomId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CupomId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CupomId",
                table: "Produtos");
        }
    }
}
