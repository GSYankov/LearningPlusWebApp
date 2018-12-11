using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPlus.Web.Migrations
{
    public partial class AddEntityNewsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsTargetRole_News_NewsId",
                table: "NewsTargetRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTargetRole",
                table: "NewsTargetRole");

            migrationBuilder.DropIndex(
                name: "IX_NewsTargetRole_NewsId",
                table: "NewsTargetRole");

            migrationBuilder.DropColumn(
                name: "LearningPlusNewsId",
                table: "NewsTargetRole");

            migrationBuilder.AlterColumn<Guid>(
                name: "NewsId",
                table: "NewsTargetRole",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTargetRole",
                table: "NewsTargetRole",
                columns: new[] { "NewsId", "TargetRole" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTargetRole_News_NewsId",
                table: "NewsTargetRole",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsTargetRole_News_NewsId",
                table: "NewsTargetRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTargetRole",
                table: "NewsTargetRole");

            migrationBuilder.AlterColumn<Guid>(
                name: "NewsId",
                table: "NewsTargetRole",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "LearningPlusNewsId",
                table: "NewsTargetRole",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTargetRole",
                table: "NewsTargetRole",
                columns: new[] { "LearningPlusNewsId", "TargetRole" });

            migrationBuilder.CreateIndex(
                name: "IX_NewsTargetRole_NewsId",
                table: "NewsTargetRole",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTargetRole_News_NewsId",
                table: "NewsTargetRole",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
