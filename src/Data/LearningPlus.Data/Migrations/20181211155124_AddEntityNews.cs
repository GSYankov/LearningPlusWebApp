using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPlus.Web.Migrations
{
    public partial class AddEntityNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    ExpiresOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsTargetRole",
                columns: table => new
                {
                    LearningPlusNewsId = table.Column<string>(nullable: false),
                    TargetRole = table.Column<int>(nullable: false),
                    NewsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTargetRole", x => new { x.LearningPlusNewsId, x.TargetRole });
                    table.ForeignKey(
                        name: "FK_NewsTargetRole_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatorId",
                table: "News",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTargetRole_NewsId",
                table: "NewsTargetRole",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsTargetRole");

            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
