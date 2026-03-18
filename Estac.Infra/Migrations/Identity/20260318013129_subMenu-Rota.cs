using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations.Identity
{
    /// <inheritdoc />
    public partial class subMenuRota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rota",
                schema: "dbo",
                table: "SubModule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubModuleId",
                schema: "dbo",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_SubModuleId",
                schema: "dbo",
                table: "RolePermission",
                column: "SubModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_SubModule_SubModuleId",
                schema: "dbo",
                table: "RolePermission",
                column: "SubModuleId",
                principalSchema: "dbo",
                principalTable: "SubModule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_SubModule_SubModuleId",
                schema: "dbo",
                table: "RolePermission");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_SubModuleId",
                schema: "dbo",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "Rota",
                schema: "dbo",
                table: "SubModule");

            migrationBuilder.DropColumn(
                name: "SubModuleId",
                schema: "dbo",
                table: "RolePermission");
        }
    }
}
