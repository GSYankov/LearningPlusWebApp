using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPlus.Web.Migrations
{
    public partial class AssessmentsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Classes_CourseId",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_CourseId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Assessments");

            migrationBuilder.AddColumn<int>(
                name: "Course",
                table: "Assessments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "Assessments");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Assessments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CourseId",
                table: "Assessments",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Classes_CourseId",
                table: "Assessments",
                column: "CourseId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
