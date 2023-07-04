using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roommates.Api.Migrations
{
    public partial class IsMain_Added_To_PostFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Sequence",
                schema: "roomates",
                table: "FilePost",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                schema: "roomates",
                table: "FilePost",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                schema: "roomates",
                table: "FilePost");

            migrationBuilder.AlterColumn<short>(
                name: "Sequence",
                schema: "roomates",
                table: "FilePost",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }
    }
}
