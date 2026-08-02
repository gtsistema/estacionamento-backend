using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoAgendamentoUniquePorCobrancaTipoJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "ConfiguracaoAgendamento");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                columns: new[] { "ConfiguracaoCobrancaId", "TipoJob" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob",
                schema: "gts",
                table: "ConfiguracaoAgendamento");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                column: "ConfiguracaoCobrancaId");
        }
    }
}
