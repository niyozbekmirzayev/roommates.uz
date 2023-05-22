using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roommates.Api.Migrations
{
    public partial class ModelsModified_Improved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedByUserId",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostUser",
                schema: "roomates");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedByUserId",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ViewedTime",
                schema: "roomates",
                table: "Posts",
                newName: "ViewedCount");

            migrationBuilder.AddColumn<short>(
                name: "Sequence",
                schema: "roomates",
                table: "FilePost",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "UserPosts",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserPostRelationType = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPosts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "roomates",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPosts_UserId",
                schema: "roomates",
                table: "UserPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPosts",
                schema: "roomates");

            migrationBuilder.DropColumn(
                name: "Sequence",
                schema: "roomates",
                table: "FilePost");

            migrationBuilder.RenameColumn(
                name: "ViewedCount",
                schema: "roomates",
                table: "Posts",
                newName: "ViewedTime");

            migrationBuilder.CreateTable(
                name: "PostUser",
                schema: "roomates",
                columns: table => new
                {
                    LikedByUsersId = table.Column<Guid>(type: "uuid", nullable: false),
                    LikedPostsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUser", x => new { x.LikedByUsersId, x.LikedPostsId });
                    table.ForeignKey(
                        name: "FK_PostUser_Posts_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalSchema: "roomates",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUser_Users_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalSchema: "roomates",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedByUserId",
                schema: "roomates",
                table: "Posts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_LikedPostsId",
                schema: "roomates",
                table: "PostUser",
                column: "LikedPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedByUserId",
                schema: "roomates",
                table: "Posts",
                column: "CreatedByUserId",
                principalSchema: "roomates",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
