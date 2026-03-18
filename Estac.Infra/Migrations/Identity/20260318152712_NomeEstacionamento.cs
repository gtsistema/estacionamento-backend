using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations.Identity
{
    /// <inheritdoc />
    public partial class NomeEstacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstacionadoId",
                schema: "dbo",
                table: "User",
                newName: "EstacionamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstacionamentoId",
                schema: "dbo",
                table: "User",
                newName: "EstacionadoId");
        }
    }
}
