using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PessoaContatoCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PessoaContato_PessoaId_Numero",
                schema: "gts",
                table: "PessoaContato");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.Sql(@"
UPDATE [gts].[PessoaContato]
SET [Telefone] = LEFT(LTRIM(RTRIM([Numero])), 20)
WHERE NULLIF(LTRIM(RTRIM([Numero])), '') IS NOT NULL;");

            migrationBuilder.Sql(@"
UPDATE [gts].[PessoaContato]
SET [Descricao] = LEFT([Descricao], 200)
WHERE [Descricao] IS NOT NULL AND LEN([Descricao]) > 200;");

            migrationBuilder.DropColumn(
                name: "Numero",
                schema: "gts",
                table: "PessoaContato");

            migrationBuilder.DropColumn(
                name: "TipoContato",
                schema: "gts",
                table: "PessoaContato");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(200)",
                maxLength: 200,
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
                table: "PessoaContato",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                schema: "gts",
                table: "PessoaContato",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoContato",
                schema: "gts",
                table: "PessoaContato",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql(@"
UPDATE [gts].[PessoaContato]
SET [Numero] = LEFT(COALESCE(LTRIM(RTRIM([Telefone])), ''), 20)
WHERE [Telefone] IS NOT NULL AND NULLIF(LTRIM(RTRIM([Telefone])), '') IS NOT NULL;");

            migrationBuilder.DropColumn(
                name: "Cpf",
                schema: "gts",
                table: "PessoaContato");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "gts",
                table: "PessoaContato");

            migrationBuilder.DropColumn(
                name: "Telefone",
                schema: "gts",
                table: "PessoaContato");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaContato_PessoaId_Numero",
                schema: "gts",
                table: "PessoaContato",
                columns: new[] { "PessoaId", "Numero" });
        }
    }
}
