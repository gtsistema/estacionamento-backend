using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MelhoriasNomeclatura1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorEstacionamento",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                newName: "ValorEstadia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorEstadia",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                newName: "ValorEstacionamento");
        }
    }
}
