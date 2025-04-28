using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class receita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MesReferente",
                table: "Receita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentagemPaga",
                table: "Receita",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoRestante",
                table: "Receita",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5332));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5365));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5400));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5416));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5450));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(9265));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(9273));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(9274));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(9275));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(7765));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 28, 8, 24, 7, 390, DateTimeKind.Local).AddTicks(7780));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MesReferente",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "PorcentagemPaga",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "SaldoRestante",
                table: "Receita");

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5520));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5591));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5610));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5641));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5672));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(9299));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(9305));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(9307));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataCriacao",
                value: new DateTime(2025, 4, 27, 17, 45, 4, 608, DateTimeKind.Local).AddTicks(7910));
        }
    }
}
