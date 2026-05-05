using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PessoaRemoveNomeFantasiaUsaDescricaoBaseInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Copia todo valor existente em NomeFantasia para Descricao (limite 150 por varchar).
            // Linhas sem NomeFantasia mantêm a Descricao que já tinham.
            migrationBuilder.Sql(@"
UPDATE [gts].[Pessoa]
SET [Descricao] = LEFT(LTRIM(RTRIM([NomeFantasia])), 150)
WHERE NULLIF(LTRIM(RTRIM([NomeFantasia])), '') IS NOT NULL;");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                schema: "gts",
                table: "Pessoa");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "gts",
                table: "Pessoa",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "gts",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                schema: "gts",
                table: "Pessoa",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.Sql(@"
UPDATE [gts].[Pessoa]
SET [NomeFantasia] = LEFT(LTRIM(RTRIM([Descricao])), 150)
WHERE [Descricao] IS NOT NULL;");
        }
    }
}
