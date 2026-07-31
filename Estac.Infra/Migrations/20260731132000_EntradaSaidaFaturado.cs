using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EntradaSaidaFaturado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Faturado",
                schema: "gts",
                table: "EntradaSaida",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFaturado",
                schema: "gts",
                table: "EntradaSaida",
                type: "datetime",
                nullable: true);

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_EntradaSaida_Faturamento' AND object_id = OBJECT_ID(N'gts.EntradaSaida'))
    DROP INDEX [IX_EntradaSaida_Faturamento] ON [gts].[EntradaSaida];
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_EntradaSaida_Faturamento' AND object_id = OBJECT_ID(N'gts.EntradaSaida'))
    CREATE NONCLUSTERED INDEX [IX_EntradaSaida_Faturamento]
    ON [gts].[EntradaSaida] ([EstacionamentoId], [TransportadoraId], [DataHoraSaida])
    WHERE [Finalizado] = 1 AND [Faturado] = 0;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_EntradaSaida_Faturamento' AND object_id = OBJECT_ID(N'gts.EntradaSaida'))
    DROP INDEX [IX_EntradaSaida_Faturamento] ON [gts].[EntradaSaida];
");

            migrationBuilder.DropColumn(
                name: "DataFaturado",
                schema: "gts",
                table: "EntradaSaida");

            migrationBuilder.DropColumn(
                name: "Faturado",
                schema: "gts",
                table: "EntradaSaida");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_EntradaSaida_Faturamento' AND object_id = OBJECT_ID(N'gts.EntradaSaida'))
    CREATE NONCLUSTERED INDEX [IX_EntradaSaida_Faturamento]
    ON [gts].[EntradaSaida] ([EstacionamentoId], [TransportadoraId], [DataHoraSaida])
    WHERE [Finalizado] = 1;
");
        }
    }
}
