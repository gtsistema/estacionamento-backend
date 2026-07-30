using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoCobrancaServicosAdicionais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CobrarLavagem",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CobrarPernoite",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CobrarServicosExtras",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConsiderarBeneficioAbastecimento",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCobranca",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorBeneficioAbastecimento",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorLavagem",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPernoite",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorServicosExtras",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CobrarLavagem",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "CobrarPernoite",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "CobrarServicosExtras",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ConsiderarBeneficioAbastecimento",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "DataCobranca",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ValorBeneficioAbastecimento",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ValorLavagem",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ValorPernoite",
                schema: "gts",
                table: "ConfiguracaoCobranca");

            migrationBuilder.DropColumn(
                name: "ValorServicosExtras",
                schema: "gts",
                table: "ConfiguracaoCobranca");
        }
    }
}
