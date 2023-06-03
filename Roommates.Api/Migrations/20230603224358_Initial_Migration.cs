using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roommates.Api.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "roomates");

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFeatures",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DynamicFeatureType = table.Column<string>(type: "varchar(24)", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: true),
                    IsExist = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    VerificationCode = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "varchar(24)", nullable: false),
                    VerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilePost",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequence = table.Column<short>(type: "smallint", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    IsTemporary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    InactivatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InactivatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityState = table.Column<string>(type: "varchar(24)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "varchar(24)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    ProfilePictureFileId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityState = table.Column<string>(type: "varchar(24)", nullable: false),
                    InactivatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Files_ProfilePictureFileId",
                        column: x => x.ProfilePictureFileId,
                        principalSchema: "roomates",
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewsCount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    InactivatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityState = table.Column<string>(type: "varchar(24)", nullable: false),
                    PostStatus = table.Column<string>(type: "varchar(24)", nullable: false),
                    InactivatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "roomates",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "roomates",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticFeatures",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    RoomsCount = table.Column<short>(type: "smallint", nullable: false),
                    IsForSelling = table.Column<bool>(type: "boolean", nullable: false),
                    PreferedClientType = table.Column<string>(type: "varchar(24)", nullable: false),
                    PricePeriodType = table.Column<string>(type: "varchar(24)", nullable: true),
                    CurrencyType = table.Column<string>(type: "varchar(24)", nullable: false),
                    EntityState = table.Column<string>(type: "varchar(24)", nullable: false),
                    InactivatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticFeatures_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "roomates",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPosts",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserPostRelationType = table.Column<string>(type: "varchar(24)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "roomates",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPosts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "roomates",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFeatures_PostId",
                schema: "roomates",
                table: "DynamicFeatures",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserId",
                schema: "roomates",
                table: "Emails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilePost_FileId",
                schema: "roomates",
                table: "FilePost",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FilePost_PostId",
                schema: "roomates",
                table: "FilePost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CreatedById",
                schema: "roomates",
                table: "Files",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                schema: "roomates",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LocationId",
                schema: "roomates",
                table: "Posts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticFeatures_PostId",
                schema: "roomates",
                table: "StaticFeatures",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPosts_PostId",
                schema: "roomates",
                table: "UserPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPosts_UserId",
                schema: "roomates",
                table: "UserPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePictureFileId",
                schema: "roomates",
                table: "Users",
                column: "ProfilePictureFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFeatures_Posts_PostId",
                schema: "roomates",
                table: "DynamicFeatures",
                column: "PostId",
                principalSchema: "roomates",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Users_UserId",
                schema: "roomates",
                table: "Emails",
                column: "UserId",
                principalSchema: "roomates",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_FilePost_Posts_PostId",
                schema: "roomates",
                table: "FilePost",
                column: "PostId",
                principalSchema: "roomates",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_CreatedById",
                schema: "roomates",
                table: "Files",
                column: "CreatedById",
                principalSchema: "roomates",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_CreatedById",
                schema: "roomates",
                table: "Files");

            migrationBuilder.DropTable(
                name: "DynamicFeatures",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Emails",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "FilePost",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "StaticFeatures",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "UserPosts",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "roomates");
        }
    }
}
