using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixConfiguracaoCobrancaValorEstacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Idempotente: cobre ambientes em que MelhoriasNomeclatura1 reverteu o rename.
            migrationBuilder.Sql(@"
IF COL_LENGTH('gts.ConfiguracaoCobranca', 'ValorEstadia') IS NOT NULL
   AND COL_LENGTH('gts.ConfiguracaoCobranca', 'ValorEstacionamento') IS NULL
    EXEC sp_rename N'gts.ConfiguracaoCobranca.ValorEstadia', N'ValorEstacionamento', N'COLUMN';
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('gts.ConfiguracaoCobranca', 'ValorEstacionamento') IS NOT NULL
   AND COL_LENGTH('gts.ConfiguracaoCobranca', 'ValorEstadia') IS NULL
    EXEC sp_rename N'gts.ConfiguracaoCobranca.ValorEstacionamento', N'ValorEstadia', N'COLUMN';
");
        }
    }
}
