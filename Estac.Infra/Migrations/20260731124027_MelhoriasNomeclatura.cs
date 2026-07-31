using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MelhoriasNomeclatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstacionamentoId",
                schema: "gts",
                table: "EntradaSaida",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FaturaItem",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaId = table.Column<int>(type: "int", nullable: false),
                    EntradaSaidaId = table.Column<int>(type: "int", nullable: false),
                    Placa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DataHoraEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraSaida = table.Column<DateTime>(type: "datetime", nullable: false),
                    TempoPermanenciaMinutos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ValorEstacionamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorLavagem = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorPernoite = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorServicosExtras = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorBeneficioAbastecimento = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturaItem_EntradaSaida_EntradaSaidaId",
                        column: x => x.EntradaSaidaId,
                        principalSchema: "gts",
                        principalTable: "EntradaSaida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturaItem_Fatura_FaturaId",
                        column: x => x.FaturaId,
                        principalSchema: "gts",
                        principalTable: "Fatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntradaSaida_Faturamento",
                schema: "gts",
                table: "EntradaSaida",
                columns: new[] { "EstacionamentoId", "TransportadoraId", "DataHoraSaida" },
                filter: "[Finalizado] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaItem_EntradaSaidaId",
                schema: "gts",
                table: "FaturaItem",
                column: "EntradaSaidaId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaItem_FaturaId_EntradaSaidaId",
                schema: "gts",
                table: "FaturaItem",
                columns: new[] { "FaturaId", "EntradaSaidaId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaSaida_Estacionamento_EstacionamentoId",
                schema: "gts",
                table: "EntradaSaida",
                column: "EstacionamentoId",
                principalSchema: "gts",
                principalTable: "Estacionamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaSaida_Estacionamento_EstacionamentoId",
                schema: "gts",
                table: "EntradaSaida");

            migrationBuilder.DropTable(
                name: "FaturaItem",
                schema: "gts");

            migrationBuilder.DropIndex(
                name: "IX_EntradaSaida_Faturamento",
                schema: "gts",
                table: "EntradaSaida");

            migrationBuilder.DropColumn(
                name: "EstacionamentoId",
                schema: "gts",
                table: "EntradaSaida");
        }
    }
}
