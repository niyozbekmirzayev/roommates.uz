using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roommates.Data.Migrations
{
    public partial class DefaultSchemeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "roomates");

            migrationBuilder.RenameTable(
                name: "Roommates",
                newName: "Roommates",
                newSchema: "roomates");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Posts",
                newSchema: "roomates");

            migrationBuilder.RenameTable(
                name: "PostRoommate",
                newName: "PostRoommate",
                newSchema: "roomates");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Locations",
                newSchema: "roomates");

            migrationBuilder.RenameTable(
                name: "FilesPosts",
                newName: "FilesPosts",
                newSchema: "roomates");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Files",
                newSchema: "roomates");

            migrationBuilder.AddColumn<int>(
                name: "EntityState",
                schema: "roomates",
                table: "Roommates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityState",
                schema: "roomates",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityState",
                schema: "roomates",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityState",
                schema: "roomates",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityState",
                schema: "roomates",
                table: "Roommates");

            migrationBuilder.DropColumn(
                name: "EntityState",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EntityState",
                schema: "roomates",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "EntityState",
                schema: "roomates",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Roommates",
                schema: "roomates",
                newName: "Roommates");

            migrationBuilder.RenameTable(
                name: "Posts",
                schema: "roomates",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "PostRoommate",
                schema: "roomates",
                newName: "PostRoommate");

            migrationBuilder.RenameTable(
                name: "Locations",
                schema: "roomates",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "FilesPosts",
                schema: "roomates",
                newName: "FilesPosts");

            migrationBuilder.RenameTable(
                name: "Files",
                schema: "roomates",
                newName: "Files");
        }
    }
}
