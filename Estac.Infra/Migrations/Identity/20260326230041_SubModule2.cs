using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations.Identity
{
    /// <inheritdoc />
    public partial class SubModule2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubModule_Module_ModuleId",
                schema: "dbo",
                table: "SubModule");

            migrationBuilder.DropForeignKey(
                name: "FK_SubModule_Module_ModuleId1",
                schema: "dbo",
                table: "SubModule");

            migrationBuilder.DropIndex(
                name: "IX_SubModule_ModuleId1",
                schema: "dbo",
                table: "SubModule");

            migrationBuilder.DropColumn(
                name: "ModuleId1",
                schema: "dbo",
                table: "SubModule");

            migrationBuilder.AddForeignKey(
                name: "FK_SubModule_Module_ModuleId",
                schema: "dbo",
                table: "SubModule",
                column: "ModuleId",
                principalSchema: "dbo",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubModule_Module_ModuleId",
                schema: "dbo",
                table: "SubModule");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId1",
                schema: "dbo",
                table: "SubModule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubModule_ModuleId1",
                schema: "dbo",
                table: "SubModule",
                column: "ModuleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SubModule_Module_ModuleId",
                schema: "dbo",
                table: "SubModule",
                column: "ModuleId",
                principalSchema: "dbo",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubModule_Module_ModuleId1",
                schema: "dbo",
                table: "SubModule",
                column: "ModuleId1",
                principalSchema: "dbo",
                principalTable: "Module",
                principalColumn: "Id");
        }
    }
}
