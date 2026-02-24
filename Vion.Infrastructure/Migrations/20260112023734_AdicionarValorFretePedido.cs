using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarValorFretePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorFrete",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorFrete",
                table: "Pedidos");
        }
    }
}
