using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoCobranca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracaoCobranca",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportadoraId = table.Column<int>(type: "int", nullable: false),
                    EstacionamentoId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    ModalidadeCobranca = table.Column<byte>(type: "tinyint", nullable: false),
                    DiaFechamento = table.Column<byte>(type: "tinyint", nullable: true),
                    RegraFechamento = table.Column<byte>(type: "tinyint", nullable: false),
                    PrazoVencimentoDias = table.Column<int>(type: "int", nullable: false),
                    EmailFinanceiro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EnvioAutomaticoEmail = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GerarFaturaAutomaticamente = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PermitirPagamentoParcial = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    JurosPercentual = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MultaPercentual = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DescontoPercentual = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    AgruparPorPlaca = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AgruparPorPeriodo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AgruparPorTransportadora = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoCobranca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoCobranca_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalSchema: "gts",
                        principalTable: "Estacionamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoCobranca_Transportadora_TransportadoraId",
                        column: x => x.TransportadoraId,
                        principalSchema: "gts",
                        principalTable: "Transportadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracaoCobrancaRegra",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfiguracaoCobrancaId = table.Column<int>(type: "int", nullable: false),
                    CobrarDiaria = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarSemanal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarQuinzenal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarMensal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarDataPersonalizada = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarLavagem = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarPernoite = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CobrarServicosExtras = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConsiderarBeneficioAbastecimento = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoCobrancaRegra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracaoCobrancaRegra_ConfiguracaoCobranca_ConfiguracaoCobrancaId",
                        column: x => x.ConfiguracaoCobrancaId,
                        principalSchema: "gts",
                        principalTable: "ConfiguracaoCobranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoCobranca_EstacionamentoId",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                column: "EstacionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoCobranca_Transportadora_Estacionamento",
                schema: "gts",
                table: "ConfiguracaoCobranca",
                columns: new[] { "TransportadoraId", "EstacionamentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracaoCobrancaRegra_ConfiguracaoCobrancaId",
                schema: "gts",
                table: "ConfiguracaoCobrancaRegra",
                column: "ConfiguracaoCobrancaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracaoCobrancaRegra",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "ConfiguracaoCobranca",
                schema: "gts");
        }
    }
}
