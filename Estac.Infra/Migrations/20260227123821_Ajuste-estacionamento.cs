using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Ajusteestacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("9552c4b8-e60e-42a6-8f97-96b8f8edd555"));

            migrationBuilder.AddColumn<int>(
                name: "CapacidadeVeiculo",
                table: "Estacionamento",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PossuiBanheiro",
                table: "Estacionamento",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PossuiSeguranca",
                table: "Estacionamento",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tamanho",
                table: "Estacionamento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeVeiculo",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "PossuiBanheiro",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "PossuiSeguranca",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Estacionamento");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpresaId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EmpresaId", "FullName", "ImagemUrl", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TemporaryPassword", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9552c4b8-e60e-42a6-8f97-96b8f8edd555"), 0, "1a503903-b30e-4c13-9e8a-2ec6038c475c", "jeancpcorrea@gmail.com", true, null, null, null, false, false, null, "JEANCPCORREA@GMAIL.COM", "jeancpcorrea@gmail.com", "@Admin123456", "65981324241", true, "LDPU32DFTNBNBJ5JQ2R7VXEHNCGE6UER", false, false, "Admin" });
        }
    }
}
