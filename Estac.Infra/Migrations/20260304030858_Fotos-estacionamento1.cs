using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Fotosestacionamento1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caminho",
                table: "EstacionamentoFoto");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "EstacionamentoFoto");

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "EstacionamentoFoto",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "EstacionamentoFoto");

            migrationBuilder.AddColumn<string>(
                name: "Caminho",
                table: "EstacionamentoFoto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "EstacionamentoFoto",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
