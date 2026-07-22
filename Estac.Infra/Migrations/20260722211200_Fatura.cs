using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Fatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fatura",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportadoraId = table.Column<int>(type: "int", nullable: false),
                    EstacionamentoId = table.Column<int>(type: "int", nullable: false),
                    ConfiguracaoCobrancaId = table.Column<int>(type: "int", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ModalidadeRecebimento = table.Column<byte>(type: "tinyint", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorRecebido = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorAcrescimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorJuros = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ValorMulta = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodoInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodoFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailEnvio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fatura_ConfiguracaoCobranca_ConfiguracaoCobrancaId",
                        column: x => x.ConfiguracaoCobrancaId,
                        principalSchema: "gts",
                        principalTable: "ConfiguracaoCobranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Fatura_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalSchema: "gts",
                        principalTable: "Estacionamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fatura_Transportadora_TransportadoraId",
                        column: x => x.TransportadoraId,
                        principalSchema: "gts",
                        principalTable: "Transportadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "Fatura",
                column: "ConfiguracaoCobrancaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_DataEmissao",
                schema: "gts",
                table: "Fatura",
                column: "DataEmissao");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_DataVencimento",
                schema: "gts",
                table: "Fatura",
                column: "DataVencimento");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_EstacionamentoId",
                schema: "gts",
                table: "Fatura",
                column: "EstacionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_Numero",
                schema: "gts",
                table: "Fatura",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_Status",
                schema: "gts",
                table: "Fatura",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_TransportadoraId",
                schema: "gts",
                table: "Fatura",
                column: "TransportadoraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fatura",
                schema: "gts");
        }
    }
}
