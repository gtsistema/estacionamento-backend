using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoAgendamentoModalidadeCobranca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Periodicidade",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                newName: "ModalidadeCobranca");

            // Periodicidade tinha valores ambíguos (quinzenal e mensal compartilhavam o valor 3).
            // O agendamento passa a espelhar a modalidade da configuração de cobrança.
            // EXEC adia a compilação: a coluna renomeada não existe quando o lote é analisado.
            migrationBuilder.Sql(
                """
                EXEC(N'
                    UPDATE agendamento
                    SET agendamento.ModalidadeCobranca = cobranca.ModalidadeCobranca
                    FROM gts.ConfiguracaoAgendamento AS agendamento
                    INNER JOIN gts.ConfiguracaoCobranca AS cobranca
                        ON cobranca.Id = agendamento.ConfiguracaoCobrancaId;
                ');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModalidadeCobranca",
                schema: "gts",
                table: "ConfiguracaoAgendamento",
                newName: "Periodicidade");
        }
    }
}
