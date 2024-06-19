using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MesDoAno = table.Column<int>(type: "int", nullable: true),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    ValorTotalDespesas = table.Column<float>(type: "real", nullable: false),
                    ValorTotalReceitas = table.Column<float>(type: "real", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDespesa = table.Column<int>(type: "int", nullable: false),
                    ValorPrevisto = table.Column<float>(type: "real", nullable: false),
                    ValorTotal = table.Column<float>(type: "real", nullable: false),
                    OrcamentoId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesa_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIpoReceita = table.Column<int>(type: "int", nullable: false),
                    ValorPrevisto = table.Column<float>(type: "real", nullable: false),
                    ValorTotal = table.Column<float>(type: "real", nullable: false),
                    OrcamentoId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receita_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaLancamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ValorTotal = table.Column<float>(type: "real", nullable: false),
                    DespesaId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaLancamento_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaLancamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ValorTotal = table.Column<float>(type: "real", nullable: false),
                    ReceitaId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceitaLancamento_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orcamento",
                columns: new[] { "Id", "Ano", "DataAtualizacao", "DataCriacao", "Descricao", "MesDoAno", "ValorTotalDespesas", "ValorTotalReceitas" },
                values: new object[] { 1, 2024, null, new DateTime(2024, 5, 3, 0, 20, 17, 922, DateTimeKind.Local).AddTicks(8594), "Viagem Cuiabá Férias", 7, 0f, 0f });

            migrationBuilder.InsertData(
                table: "Orcamento",
                columns: new[] { "Id", "Ano", "DataAtualizacao", "DataCriacao", "Descricao", "MesDoAno", "ValorTotalDespesas", "ValorTotalReceitas" },
                values: new object[] { 2, 2024, null, new DateTime(2024, 5, 3, 0, 20, 17, 922, DateTimeKind.Local).AddTicks(8610), "Despesa Mensal", 5, 0f, 0f });

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_OrcamentoId",
                table: "Despesa",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaLancamento_DespesaId",
                table: "DespesaLancamento",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_OrcamentoId",
                table: "Receita",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaLancamento_ReceitaId",
                table: "ReceitaLancamento",
                column: "ReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesaLancamento");

            migrationBuilder.DropTable(
                name: "ReceitaLancamento");

            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Orcamento");
        }
    }
}
