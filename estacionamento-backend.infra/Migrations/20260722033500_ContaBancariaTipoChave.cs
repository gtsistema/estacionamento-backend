using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ContaBancariaTipoChave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TipoChave",
                schema: "gts",
                table: "ContaBancaria",
                type: "tinyint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoChave",
                schema: "gts",
                table: "ContaBancaria");
        }
    }
}
