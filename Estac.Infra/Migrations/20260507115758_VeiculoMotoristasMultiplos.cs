using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class VeiculoMotoristasMultiplos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotoristaVeiculo_Motorista",
                schema: "gts",
                table: "MotoristaVeiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_MotoristaVeiculo_Veiculo",
                schema: "gts",
                table: "MotoristaVeiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Motorista_MotoristaId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_MotoristaId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.Sql(@"
                INSERT INTO [gts].[MotoristaVeiculo] ([MotoristaId], [VeiculoId], [Descricao])
                SELECT v.[MotoristaId], v.[Id], NULL
                FROM [gts].[Veiculo] v
                WHERE v.[MotoristaId] IS NOT NULL
                  AND NOT EXISTS (
                      SELECT 1
                      FROM [gts].[MotoristaVeiculo] mv
                      WHERE mv.[MotoristaId] = v.[MotoristaId]
                        AND mv.[VeiculoId] = v.[Id]
                  );
            ");

            migrationBuilder.DropColumn(
                name: "MotoristaId",
                schema: "gts",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_MotoristaVeiculo_VeiculoId",
                schema: "gts",
                table: "MotoristaVeiculo");

            migrationBuilder.DropIndex(
                name: "IX_MotoristaVeiculo_MotoristaId_VeiculoId",
                schema: "gts",
                table: "MotoristaVeiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotoristaVeiculo",
                schema: "gts",
                table: "MotoristaVeiculo");

            migrationBuilder.RenameTable(
                name: "MotoristaVeiculo",
                schema: "gts",
                newName: "VeiculoMotoristas",
                newSchema: "gts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VeiculoMotoristas",
                schema: "gts",
                table: "VeiculoMotoristas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoMotoristas_VeiculoId",
                schema: "gts",
                table: "VeiculoMotoristas",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoMotoristas_MotoristaId_VeiculoId",
                schema: "gts",
                table: "VeiculoMotoristas",
                columns: new[] { "MotoristaId", "VeiculoId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoMotoristas_Motorista",
                schema: "gts",
                table: "VeiculoMotoristas",
                column: "MotoristaId",
                principalSchema: "gts",
                principalTable: "Motorista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoMotoristas_Veiculo",
                schema: "gts",
                table: "VeiculoMotoristas",
                column: "VeiculoId",
                principalSchema: "gts",
                principalTable: "Veiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoMotoristas_Motorista",
                schema: "gts",
                table: "VeiculoMotoristas");

            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoMotoristas_Veiculo",
                schema: "gts",
                table: "VeiculoMotoristas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VeiculoMotoristas",
                schema: "gts",
                table: "VeiculoMotoristas");

            migrationBuilder.DropIndex(
                name: "IX_VeiculoMotoristas_VeiculoId",
                schema: "gts",
                table: "VeiculoMotoristas");

            migrationBuilder.DropIndex(
                name: "IX_VeiculoMotoristas_MotoristaId_VeiculoId",
                schema: "gts",
                table: "VeiculoMotoristas");

            migrationBuilder.RenameTable(
                name: "VeiculoMotoristas",
                schema: "gts",
                newName: "MotoristaVeiculo",
                newSchema: "gts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotoristaVeiculo",
                schema: "gts",
                table: "MotoristaVeiculo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaVeiculo_VeiculoId",
                schema: "gts",
                table: "MotoristaVeiculo",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaVeiculo_MotoristaId_VeiculoId",
                schema: "gts",
                table: "MotoristaVeiculo",
                columns: new[] { "MotoristaId", "VeiculoId" },
                unique: true);

            migrationBuilder.AddColumn<int>(
                name: "MotoristaId",
                schema: "gts",
                table: "Veiculo",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(@"
                ;WITH PrimeirosVinculos AS (
                    SELECT mv.VeiculoId, MIN(mv.MotoristaId) AS MotoristaId
                    FROM [gts].[MotoristaVeiculo] mv
                    GROUP BY mv.VeiculoId
                )
                UPDATE v
                SET v.[MotoristaId] = p.MotoristaId
                FROM [gts].[Veiculo] v
                INNER JOIN PrimeirosVinculos p ON p.VeiculoId = v.Id;
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_MotoristaId",
                schema: "gts",
                table: "Veiculo",
                column: "MotoristaId",
                unique: true,
                filter: "[MotoristaId] IS NOT NULL");

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
                name: "FK_MotoristaVeiculo_Motorista",
                schema: "gts",
                table: "MotoristaVeiculo",
                column: "MotoristaId",
                principalSchema: "gts",
                principalTable: "Motorista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotoristaVeiculo_Veiculo",
                schema: "gts",
                table: "MotoristaVeiculo",
                column: "VeiculoId",
                principalSchema: "gts",
                principalTable: "Veiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
