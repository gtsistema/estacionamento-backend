using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations.Identity
{
    /// <inheritdoc />
    public partial class DeletarCascata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_SubModule_SubModuleId",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_SubModule_SubModuleId",
                schema: "dbo",
                table: "Permission",
                column: "SubModuleId",
                principalSchema: "dbo",
                principalTable: "SubModule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_SubModule_SubModuleId",
                schema: "dbo",
                table: "Permission");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_SubModule_SubModuleId",
                schema: "dbo",
                table: "Permission",
                column: "SubModuleId",
                principalSchema: "dbo",
                principalTable: "SubModule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
