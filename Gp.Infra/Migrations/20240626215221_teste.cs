using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8926));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8945));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8956));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8965));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8971));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8979));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 646, DateTimeKind.Local).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 647, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 647, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 647, DateTimeKind.Local).AddTicks(751));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 647, DateTimeKind.Local).AddTicks(764));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 18, 52, 20, 647, DateTimeKind.Local).AddTicks(769));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(968));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(990));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(999));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(1009));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(1027));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(5018));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(5025));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(3507));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(3525));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 15, 3, 55, 779, DateTimeKind.Local).AddTicks(3533));
        }
    }
}
