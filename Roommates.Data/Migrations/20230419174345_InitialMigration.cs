using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Roommates.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "roomates");

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
                    EntityState = table.Column<int>(type: "integer", nullable: false),
                    AuthorRoommateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    EntityState = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roommates",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    IsPhoneNumberVerified = table.Column<bool>(type: "boolean", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    EntityState = table.Column<int>(type: "integer", nullable: false),
                    ProfilePictureFileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roommates", x => x.Id);
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
                    Address = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    RoomsCount = table.Column<short>(type: "smallint", nullable: false),
                    IsForSelling = table.Column<bool>(type: "boolean", nullable: false),
                    ViewedTime = table.Column<long>(type: "bigint", nullable: false),
                    PreferedRoommateGender = table.Column<int>(type: "integer", nullable: false),
                    PricePeriodType = table.Column<int>(type: "integer", nullable: true),
                    CurrencyType = table.Column<int>(type: "integer", nullable: false),
                    CreatedByRoommateId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityState = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_Posts_Roommates_CreatedByRoommateId",
                        column: x => x.CreatedByRoommateId,
                        principalSchema: "roomates",
                        principalTable: "Roommates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesPosts",
                schema: "roomates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "roomates",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostRoommate",
                schema: "roomates",
                columns: table => new
                {
                    LikedByRoommatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    LikedPostsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRoommate", x => new { x.LikedByRoommatesId, x.LikedPostsId });
                    table.ForeignKey(
                        name: "FK_PostRoommate_Posts_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalSchema: "roomates",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostRoommate_Roommates_LikedByRoommatesId",
                        column: x => x.LikedByRoommatesId,
                        principalSchema: "roomates",
                        principalTable: "Roommates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesPosts_PostId",
                schema: "roomates",
                table: "FilesPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRoommate_LikedPostsId",
                schema: "roomates",
                table: "PostRoommate",
                column: "LikedPostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedByRoommateId",
                schema: "roomates",
                table: "Posts",
                column: "CreatedByRoommateId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LocationId",
                schema: "roomates",
                table: "Posts",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "FilesPosts",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "PostRoommate",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "roomates");

            migrationBuilder.DropTable(
                name: "Roommates",
                schema: "roomates");
        }
    }
}
