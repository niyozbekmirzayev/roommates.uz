using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roommates.Api.Migrations
{
    public partial class IsTemporary_Added_To_File : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTemporary",
                schema: "roomates",
                table: "Files",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTemporary",
                schema: "roomates",
                table: "Files");
        }
    }
}
