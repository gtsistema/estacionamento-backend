using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class NovosCamposUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstacionadoId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportadoraId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transportadora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportadora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportadora_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_EstacionadoId",
                table: "User",
                column: "EstacionadoId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PessoaId",
                table: "User",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TransportadoraId",
                table: "User",
                column: "TransportadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportadora_PessoaId",
                table: "Transportadora",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Estacionamento_EstacionadoId",
                table: "User",
                column: "EstacionadoId",
                principalTable: "Estacionamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Pessoa_PessoaId",
                table: "User",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Transportadora_TransportadoraId",
                table: "User",
                column: "TransportadoraId",
                principalTable: "Transportadora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Estacionamento_EstacionadoId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Pessoa_PessoaId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Transportadora_TransportadoraId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Transportadora");

            migrationBuilder.DropIndex(
                name: "IX_User_EstacionadoId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PessoaId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_TransportadoraId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EstacionadoId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TransportadoraId",
                table: "User");
        }
    }
}
