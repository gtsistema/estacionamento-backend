using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TransportadoraVeiculoMotoristaVinculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotoristaId",
                schema: "gts",
                table: "Veiculo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportadoraId",
                schema: "gts",
                table: "Veiculo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_MotoristaId",
                schema: "gts",
                table: "Veiculo",
                column: "MotoristaId",
                unique: true,
                filter: "[MotoristaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_TransportadoraId",
                schema: "gts",
                table: "Veiculo",
                column: "TransportadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Motorista_MotoristaId",
                schema: "gts",
                table: "Veiculo",
                column: "MotoristaId",
                principalSchema: "gts",
                principalTable: "Motorista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Transportadora_TransportadoraId",
                schema: "gts",
                table: "Veiculo",
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
                name: "FK_Veiculo_Motorista_MotoristaId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Transportadora_TransportadoraId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_MotoristaId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_TransportadoraId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "MotoristaId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "TransportadoraId",
                schema: "gts",
                table: "Veiculo");
        }
    }
}
