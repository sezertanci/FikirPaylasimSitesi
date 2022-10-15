using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EmailConfirmation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfirmation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entry",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryComment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryComment_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryComment_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryFavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryFavorite_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryFavorite_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryTag",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryTag_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "dbo",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryVote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryVote_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryCommentFavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCommentFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCommentFavorite_EntryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "EntryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryCommentFavorite_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryCommentVote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCommentVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCommentVote_EntryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "EntryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmation_CreatedDate",
                schema: "dbo",
                table: "EmailConfirmation",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmation_Deleted",
                schema: "dbo",
                table: "EmailConfirmation",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_CreatedDate",
                schema: "dbo",
                table: "Entry",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_Deleted",
                schema: "dbo",
                table: "Entry",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_UserId",
                schema: "dbo",
                table: "Entry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_CreatedDate",
                schema: "dbo",
                table: "EntryComment",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_Deleted",
                schema: "dbo",
                table: "EntryComment",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_EntryId",
                schema: "dbo",
                table: "EntryComment",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_UserId",
                schema: "dbo",
                table: "EntryComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_CreatedDate",
                schema: "dbo",
                table: "EntryCommentFavorite",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_Deleted",
                schema: "dbo",
                table: "EntryCommentFavorite",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_EntryCommentId",
                schema: "dbo",
                table: "EntryCommentFavorite",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_UserId",
                schema: "dbo",
                table: "EntryCommentFavorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentVote_CreatedDate",
                schema: "dbo",
                table: "EntryCommentVote",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentVote_Deleted",
                schema: "dbo",
                table: "EntryCommentVote",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentVote_EntryCommentId",
                schema: "dbo",
                table: "EntryCommentVote",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryFavorite_CreatedDate",
                schema: "dbo",
                table: "EntryFavorite",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntryFavorite_Deleted",
                schema: "dbo",
                table: "EntryFavorite",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntryFavorite_EntryId",
                schema: "dbo",
                table: "EntryFavorite",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryFavorite_UserId",
                schema: "dbo",
                table: "EntryFavorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTag_CreatedDate",
                schema: "dbo",
                table: "EntryTag",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTag_Deleted",
                schema: "dbo",
                table: "EntryTag",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTag_EntryId",
                schema: "dbo",
                table: "EntryTag",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTag_TagId",
                schema: "dbo",
                table: "EntryTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryVote_CreatedDate",
                schema: "dbo",
                table: "EntryVote",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntryVote_Deleted",
                schema: "dbo",
                table: "EntryVote",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EntryVote_EntryId",
                schema: "dbo",
                table: "EntryVote",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CreatedDate",
                schema: "dbo",
                table: "Tag",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Deleted",
                schema: "dbo",
                table: "Tag",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedDate",
                schema: "dbo",
                table: "User",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_User_Deleted",
                schema: "dbo",
                table: "User",
                column: "Deleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailConfirmation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryCommentFavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryCommentVote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryFavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryTag",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryVote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryComment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Entry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
