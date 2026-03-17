using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations.Identity
{
    /// <inheritdoc />
    public partial class RolePermisson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                schema: "dbo",
                table: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                schema: "dbo",
                newName: "UserPermission",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermission",
                schema: "dbo",
                table: "UserPermission",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermission",
                schema: "dbo",
                table: "UserPermission");

            migrationBuilder.RenameTable(
                name: "UserPermission",
                schema: "dbo",
                newName: "UserPermissions",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                schema: "dbo",
                table: "UserPermissions",
                column: "Id");
        }
    }
}
