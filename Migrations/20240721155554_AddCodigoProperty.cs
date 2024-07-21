using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnet_store.Migrations
{
    /// <inheritdoc />
    public partial class AddCodigoProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Servicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Fornecedores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Fornecedores");
        }
    }
}
