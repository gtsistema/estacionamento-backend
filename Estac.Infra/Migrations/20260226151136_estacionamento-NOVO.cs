using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class estacionamentoNOVO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PessoaPapel_PessoaId_TipoPapel",
                table: "PessoaPapel");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Motorista");

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estacionamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estacionamento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamento_PessoaId",
                table: "Estacionamento",
                column: "PessoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Pessoa");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Motorista",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Motorista",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PessoaPapel_PessoaId_TipoPapel",
                table: "PessoaPapel",
                columns: new[] { "PessoaId", "TipoPapel" },
                unique: true);
        }
    }
}
