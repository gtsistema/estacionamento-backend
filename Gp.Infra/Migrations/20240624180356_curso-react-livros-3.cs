using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class cursoreactlivros3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataLancamento",
                table: "Livro",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataLancamento",
                table: "Livro",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3848));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3858));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3875));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(7044));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(5842));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(5857));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 13, 42, 22, 325, DateTimeKind.Local).AddTicks(5864));
        }
    }
}
