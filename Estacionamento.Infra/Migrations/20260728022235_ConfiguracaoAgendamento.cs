using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracaoAgendamento",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfiguracaoCobrancaId = table.Column<int>(type: "int", nullable: false),
                    TipoJob = table.Column<int>(type: "int", nullable: false),
                    Periodicidade = table.Column<int>(type: "int", nullable: false),
                    Intervalo = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DiaSemana = table.Column<int>(type: "int", nullable: true),
                    DiaMes = table.Column<int>(type: "int", nullable: true),
                    HoraExecucao = table.Column<TimeSpan>(type: "time", nullable: false),
                    UltimaExecucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProximaExecucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoAgendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoAgendamento_ConfiguracaoCobranca_ConfiguracaoCobrancaId",
                        column: x => x.ConfiguracaoCobrancaId,
                        principalSchema: "gts",
                        principalTable: "ConfiguracaoCobranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAgendamento_Ativo",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                column: "ConfiguracaoCobrancaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAgendamento_TipoJob",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                column: "TipoJob");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracaoAgendamento",
                schema: "gts");
        }
    }
}
