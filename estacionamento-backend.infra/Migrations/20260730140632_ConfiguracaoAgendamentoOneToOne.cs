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
            migrationBuilder.Sql(
                """
                IF EXISTS
                (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob'
                      AND object_id = OBJECT_ID(N'[gts].[ConfiguracaoAgendamento]')
                )
                DROP INDEX [IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob]
                    ON [gts].[ConfiguracaoAgendamento];
                """);

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

            migrationBuilder.Sql(
                """
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId'
                      AND object_id = OBJECT_ID(N'[gts].[ConfiguracaoAgendamento]')
                )
                CREATE UNIQUE INDEX [IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId]
                    ON [gts].[ConfiguracaoAgendamento] ([ConfiguracaoCobrancaId]);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                IF EXISTS
                (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId'
                      AND object_id = OBJECT_ID(N'[gts].[ConfiguracaoAgendamento]')
                )
                DROP INDEX [IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId]
                    ON [gts].[ConfiguracaoAgendamento];
                """);

            migrationBuilder.Sql(
                """
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob'
                      AND object_id = OBJECT_ID(N'[gts].[ConfiguracaoAgendamento]')
                )
                CREATE UNIQUE INDEX [IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId_TipoJob]
                    ON [gts].[ConfiguracaoAgendamento] ([ConfiguracaoCobrancaId], [TipoJob]);
                """);
        }
    }
}
