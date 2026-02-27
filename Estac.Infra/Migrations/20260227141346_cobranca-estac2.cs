using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class cobrancaestac2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Estacionamento");

            migrationBuilder.AddColumn<byte>(
                name: "CobrancaPorcentagem",
                table: "Estacionamento",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CobrancaValor",
                table: "Estacionamento",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelCpf",
                table: "Estacionamento",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResposanvelLegal",
                table: "Estacionamento",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TamanhoTerreno",
                table: "Estacionamento",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TipoCobranca",
                table: "Estacionamento",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CobrancaPorcentagem",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "CobrancaValor",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "ResponsavelCpf",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "ResposanvelLegal",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "TamanhoTerreno",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "TipoCobranca",
                table: "Estacionamento");

            migrationBuilder.AddColumn<bool>(
                name: "Tamanho",
                table: "Estacionamento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
