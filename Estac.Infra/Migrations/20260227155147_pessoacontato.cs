using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class pessoacontato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaTelefone");

            migrationBuilder.CreateTable(
                name: "PessoaContato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    TipoContato = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaContato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaContato_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaContato_PessoaId",
                table: "PessoaContato",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaContato_PessoaId_Numero",
                table: "PessoaContato",
                columns: new[] { "PessoaId", "Numero" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaContato");

            migrationBuilder.CreateTable(
                name: "PessoaTelefone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    TipoTelefone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaTelefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaTelefone_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaTelefone_PessoaId",
                table: "PessoaTelefone",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaTelefone_PessoaId_Numero",
                table: "PessoaTelefone",
                columns: new[] { "PessoaId", "Numero" });
        }
    }
}
