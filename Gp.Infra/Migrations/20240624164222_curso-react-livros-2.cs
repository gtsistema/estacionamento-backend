using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class cursoreactlivros2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Livro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livro",
                table: "Livro",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Livro",
                table: "Livro");

            migrationBuilder.RenameTable(
                name: "Livro",
                newName: "Book");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1522));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1547));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1558));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1566));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1575));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1591));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(1599));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(4749));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(4754));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(3483));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(3501));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 24, 11, 16, 58, 946, DateTimeKind.Local).AddTicks(3508));
        }
    }
}
