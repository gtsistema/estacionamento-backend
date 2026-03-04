using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TestandoFOTOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "EstacionamentoFoto");

            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "EstacionamentoFoto");

            migrationBuilder.RenameColumn(
                name: "Imagem",
                table: "EstacionamentoFoto",
                newName: "Foto");

            migrationBuilder.RenameColumn(
                name: "DataUpload",
                table: "EstacionamentoFoto",
                newName: "DataCriacao");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "EstacionamentoFoto",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "EstacionamentoFoto");

            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "EstacionamentoFoto",
                newName: "Imagem");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "EstacionamentoFoto",
                newName: "DataUpload");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "EstacionamentoFoto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "EstacionamentoFoto",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
