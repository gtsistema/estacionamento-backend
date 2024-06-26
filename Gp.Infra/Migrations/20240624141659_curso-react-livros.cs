using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gp.Infra.Migrations
{
    public partial class cursoreactlivros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataLancamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4728));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4738));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4756));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4766));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4775));

            migrationBuilder.UpdateData(
                table: "Despesa",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "Orcamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(8093));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(6849));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(6864));

            migrationBuilder.UpdateData(
                table: "Receita",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2024, 6, 19, 15, 26, 46, 569, DateTimeKind.Local).AddTicks(6873));
        }
    }
}
