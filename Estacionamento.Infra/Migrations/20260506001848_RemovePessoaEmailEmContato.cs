using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemovePessoaEmailEmContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE c
SET c.Email = LEFT(LTRIM(RTRIM(p.Email)), 150)
FROM [gts].[PessoaContato] AS c
INNER JOIN [gts].[Pessoa] AS p ON p.Id = c.PessoaId
WHERE c.[Principal] = 1
  AND (c.Email IS NULL OR LTRIM(RTRIM(c.Email)) = '')
  AND p.Email IS NOT NULL
  AND NULLIF(LTRIM(RTRIM(p.Email)), '') IS NOT NULL;");

            migrationBuilder.Sql(@"
INSERT INTO [gts].[PessoaContato] ([PessoaId], [Principal], [Descricao], [Cpf], [Telefone], [Email], [Observacao])
SELECT p.[Id], 1, N'Principal', NULL, NULL, LEFT(LTRIM(RTRIM(p.[Email])), 150), NULL
FROM [gts].[Pessoa] AS p
WHERE NULLIF(LTRIM(RTRIM(p.[Email])), '') IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM [gts].[PessoaContato] AS c WHERE c.PessoaId = p.Id);");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "gts",
                table: "Pessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "gts",
                table: "Pessoa",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.Sql(@"
UPDATE p
SET p.Email = LEFT(LTRIM(RTRIM(s.Email)), 150)
FROM [gts].[Pessoa] AS p
OUTER APPLY (
    SELECT TOP 1 c.Email
    FROM [gts].[PessoaContato] AS c
    WHERE c.PessoaId = p.Id
      AND c.Email IS NOT NULL
      AND NULLIF(LTRIM(RTRIM(c.Email)), '') IS NOT NULL
    ORDER BY CASE WHEN c.[Principal] = 1 THEN 0 ELSE 1 END
) AS s
WHERE s.Email IS NOT NULL;");
        }
    }
}
