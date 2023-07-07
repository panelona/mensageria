using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Pedidos.Migrations
{
    /// <inheritdoc />
    public partial class MudancaAtributoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusPedido",
                table: "Pedidos");
        }
    }
}
