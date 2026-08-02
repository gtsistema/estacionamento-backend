using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ContaBancariaTransportadora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportadoraId",
                schema: "gts",
                table: "ContaBancaria",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancaria_TransportadoraId",
                schema: "gts",
                table: "ContaBancaria",
                column: "TransportadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaBancaria_Transportadora_TransportadoraId",
                schema: "gts",
                table: "ContaBancaria",
                column: "TransportadoraId",
                principalSchema: "gts",
                principalTable: "Transportadora",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaBancaria_Transportadora_TransportadoraId",
                schema: "gts",
                table: "ContaBancaria");

            migrationBuilder.DropIndex(
                name: "IX_ContaBancaria_TransportadoraId",
                schema: "gts",
                table: "ContaBancaria");

            migrationBuilder.DropColumn(
                name: "TransportadoraId",
                schema: "gts",
                table: "ContaBancaria");
        }
    }
}
