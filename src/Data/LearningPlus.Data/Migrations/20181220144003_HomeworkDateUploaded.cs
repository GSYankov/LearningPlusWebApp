using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPlus.Web.Migrations
{
    public partial class HomeworkDateUploaded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedOn",
                table: "HomeWorks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedOn",
                table: "HomeWorks");
        }
    }
}
