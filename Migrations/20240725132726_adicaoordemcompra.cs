using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnet_store.Migrations
{
    /// <inheritdoc />
    public partial class adicaoordemcompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_OrdensCompra_OrdemCompraId",
                table: "Pedidos");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_OrdensCompra_OrdemCompraId",
                table: "Pedidos",
                column: "OrdemCompraId",
                principalTable: "OrdensCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_OrdensCompra_OrdemCompraId",
                table: "Pedidos");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_OrdensCompra_OrdemCompraId",
                table: "Pedidos",
                column: "OrdemCompraId",
                principalTable: "OrdensCompra",
                principalColumn: "Id");
        }
    }
}
