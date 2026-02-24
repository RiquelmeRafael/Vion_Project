using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCupomToPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CupomId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDesconto",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CupomId",
                table: "Pedidos",
                column: "CupomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cupons_CupomId",
                table: "Pedidos",
                column: "CupomId",
                principalTable: "Cupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cupons_CupomId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CupomId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CupomId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ValorDesconto",
                table: "Pedidos");
        }
    }
}
