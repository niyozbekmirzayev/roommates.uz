using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roommates.Api.Migrations
{
    public partial class ForeignKeys_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePictureFileId",
                schema: "roomates",
                table: "Users",
                column: "ProfilePictureFileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPosts_PostId",
                schema: "roomates",
                table: "UserPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedByUserId",
                schema: "roomates",
                table: "Posts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LocationId",
                schema: "roomates",
                table: "Posts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AuthorUserId",
                schema: "roomates",
                table: "Locations",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_AuthorUserId",
                schema: "roomates",
                table: "Files",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilePost_FileId",
                schema: "roomates",
                table: "FilePost",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePost_Files_FileId",
                schema: "roomates",
                table: "FilePost",
                column: "FileId",
                principalSchema: "roomates",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_AuthorUserId",
                schema: "roomates",
                table: "Files",
                column: "AuthorUserId",
                principalSchema: "roomates",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Users_AuthorUserId",
                schema: "roomates",
                table: "Locations",
                column: "AuthorUserId",
                principalSchema: "roomates",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Locations_LocationId",
                schema: "roomates",
                table: "Posts",
                column: "LocationId",
                principalSchema: "roomates",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedByUserId",
                schema: "roomates",
                table: "Posts",
                column: "CreatedByUserId",
                principalSchema: "roomates",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_Posts_PostId",
                schema: "roomates",
                table: "UserPosts",
                column: "PostId",
                principalSchema: "roomates",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Files_ProfilePictureFileId",
                schema: "roomates",
                table: "Users",
                column: "ProfilePictureFileId",
                principalSchema: "roomates",
                principalTable: "Files",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePost_Files_FileId",
                schema: "roomates",
                table: "FilePost");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_AuthorUserId",
                schema: "roomates",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Users_AuthorUserId",
                schema: "roomates",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Locations_LocationId",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedByUserId",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPosts_Posts_PostId",
                schema: "roomates",
                table: "UserPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Files_ProfilePictureFileId",
                schema: "roomates",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePictureFileId",
                schema: "roomates",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserPosts_PostId",
                schema: "roomates",
                table: "UserPosts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedByUserId",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_LocationId",
                schema: "roomates",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AuthorUserId",
                schema: "roomates",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Files_AuthorUserId",
                schema: "roomates",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_FilePost_FileId",
                schema: "roomates",
                table: "FilePost");
        }
    }
}
