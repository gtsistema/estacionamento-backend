using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Responsavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResposanvelLegal",
                schema: "gts",
                table: "Estacionamento",
                newName: "ResponsavelLegal");

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelCpf",
                schema: "gts",
                table: "Transportadora",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelEmail",
                schema: "gts",
                table: "Transportadora",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelLegal",
                schema: "gts",
                table: "Transportadora",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelTelefone",
                schema: "gts",
                table: "Transportadora",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelEmail",
                schema: "gts",
                table: "Estacionamento",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelTelefone",
                schema: "gts",
                table: "Estacionamento",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsavelCpf",
                schema: "gts",
                table: "Transportadora");

            migrationBuilder.DropColumn(
                name: "ResponsavelEmail",
                schema: "gts",
                table: "Transportadora");

            migrationBuilder.DropColumn(
                name: "ResponsavelLegal",
                schema: "gts",
                table: "Transportadora");

            migrationBuilder.DropColumn(
                name: "ResponsavelTelefone",
                schema: "gts",
                table: "Transportadora");

            migrationBuilder.DropColumn(
                name: "ResponsavelEmail",
                schema: "gts",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "ResponsavelTelefone",
                schema: "gts",
                table: "Estacionamento");

            migrationBuilder.RenameColumn(
                name: "ResponsavelLegal",
                schema: "gts",
                table: "Estacionamento",
                newName: "ResposanvelLegal");
        }
    }
}
