using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Motorista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportadoraId",
                schema: "gts",
                table: "Motorista",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_TransportadoraId",
                schema: "gts",
                table: "Motorista",
                column: "TransportadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorista_Transportadora_TransportadoraId",
                schema: "gts",
                table: "Motorista",
                column: "TransportadoraId",
                principalSchema: "gts",
                principalTable: "Transportadora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorista_Transportadora_TransportadoraId",
                schema: "gts",
                table: "Motorista");

            migrationBuilder.DropIndex(
                name: "IX_Motorista_TransportadoraId",
                schema: "gts",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "TransportadoraId",
                schema: "gts",
                table: "Motorista");
        }
    }
}
