using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoAgendamentoOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob",
                schema: "gts",
                table: "ConfiguracaoAgendamento");

            migrationBuilder.Sql(
                """
                WITH AgendamentosDuplicados AS
                (
                    SELECT
                        Id,
                        ROW_NUMBER() OVER
                        (
                            PARTITION BY ConfiguracaoCobrancaId
                            ORDER BY Ativo DESC, DataAtualizacao DESC, DataCadastro DESC, Id
                        ) AS Ordem
                    FROM gts.ConfiguracaoAgendamento
                )
                DELETE FROM AgendamentosDuplicados
                WHERE Ordem > 1;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                column: "ConfiguracaoCobrancaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
