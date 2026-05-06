using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EntradaSaidaStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                schema: "gts",
                table: "EntradaSaida",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                schema: "gts",
                table: "EntradaSaida",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.Sql("""
                UPDATE [gts].[EntradaSaida]
                SET [Status] = 1
                WHERE [Finalizado] = 1;

                UPDATE [gts].[EntradaSaida]
                SET [Status] = 2
                WHERE [Finalizado] = 0 AND [PermanenciaSuspensa] = 1;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                schema: "gts",
                table: "EntradaSaida");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "gts",
                table: "EntradaSaida");
        }
    }
}
