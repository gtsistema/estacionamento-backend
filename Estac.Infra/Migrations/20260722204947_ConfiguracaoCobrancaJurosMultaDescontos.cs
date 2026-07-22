using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoCobrancaJurosMultaDescontos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescontoPercentual",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)1);

            migrationBuilder.AlterColumn<decimal>(
                name: "MultaPercentual",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "JurosPercentual",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AplicarAcrescimoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicarDescontoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicarJuros",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicarMulta",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorAcrescimoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDescontoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicarAcrescimoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "AplicarDescontoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "AplicarJuros",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "AplicarMulta",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ValorAcrescimoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ValorDescontoFixo",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<decimal>(
                name: "MultaPercentual",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "JurosPercentual",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoPercentual",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(5,2)",
                nullable: true);
        }
    }
}
