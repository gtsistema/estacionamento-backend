using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class testando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1761));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(1771));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 28, 9, 52, 46, 889, DateTimeKind.Local).AddTicks(3848));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5451));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5463));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5487));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(5495));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(8470));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(8475));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 26, 19, 7, 8, 863, DateTimeKind.Local).AddTicks(7458));
        }
    }
}
