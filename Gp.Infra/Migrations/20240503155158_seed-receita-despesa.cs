using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class seedreceitadespesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Receita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Despesa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Despesa",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Descricao", "OrcamentoId", "TipoDespesa", "ValorPrevisto", "ValorTotal" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1162), "Gás", 2, 2, 43.5f, 0f },
                    { 2, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1223), "Aluguel", 2, 4, 1250f, 0f },
                    { 3, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1247), "Combustivel", 2, 14, 500f, 0f },
                    { 4, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1264), "Remedios", 2, 9, 150f, 0f },
                    { 5, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1287), "Luz", 2, 1, 260f, 0f },
                    { 6, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1301), "CartaoCredito", 2, 15, 1300f, 0f },
                    { 7, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1323), "Alimentacao", 2, 13, 1000f, 0f },
                    { 8, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(1339), "Condominio", 2, 5, 450f, 0f }
                });

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(8177));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(8193));

            migrationBuilder.InsertData(
                table: "Receita",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Descricao", "OrcamentoId", "TIpoReceita", "ValorPrevisto", "ValorTotal" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(5602), "SalarioJean", 2, 1, 3240f, 0f },
                    { 2, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(5641), "FlexBenner", 2, 6, 1250f, 0f },
                    { 3, null, new DateTime(2024, 5, 3, 12, 51, 58, 299, DateTimeKind.Local).AddTicks(5657), "Bpc", 2, 2, 1450f, 0f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Despesa");

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 5, 3, 0, 20, 17, 922, DateTimeKind.Local).AddTicks(8594));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 5, 3, 0, 20, 17, 922, DateTimeKind.Local).AddTicks(8610));
        }
    }
}
