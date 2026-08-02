using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveConfiguracaoCobrancaRegra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF OBJECT_ID(N'[gts].[ConfiguracaoCobrancaRegra]', N'U') IS NOT NULL
                    DROP TABLE [gts].[ConfiguracaoCobrancaRegra];
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracaoCobrancaRegra",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfiguracaoCobrancaId = table.Column<int>(type: "int", nullable: false),
                    CobrarDataPersonalizada = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarDiaria = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarLavagem = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarMensal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarPernoite = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarQuinzenal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarSemanal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarServicosExtras = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConsiderarBeneficioAbastecimento = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoCobrancaRegra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoCobrancaRegra_ConfiguracaoCobranca_ConfiguracaoCobrancaId",
                        column: x => x.ConfiguracaoCobrancaId,
                        principalSchema: "gts",
                        principalTable: "ConfiguracaoCobranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoCobrancaRegra_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "ConfiguracaoCobrancaRegra",
                column: "ConfiguracaoCobrancaId",
                unique: true);
        }
    }
}
