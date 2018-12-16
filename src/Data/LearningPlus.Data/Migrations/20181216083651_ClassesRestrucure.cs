using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPlus.Web.Migrations
{
    public partial class ClassesRestrucure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsClasses");

            migrationBuilder.AddColumn<Guid>(
                name: "LearningPlusClassId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LearningPlusClassId",
                table: "AspNetUsers",
                column: "LearningPlusClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_LearningPlusClassId",
                table: "AspNetUsers",
                column: "LearningPlusClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_LearningPlusClassId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LearningPlusClassId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LearningPlusClassId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "StudentsClasses",
                columns: table => new
                {
                    StudentId = table.Column<string>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsClasses", x => new { x.StudentId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_StudentsClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsClasses_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsClasses_ClassId",
                table: "StudentsClasses",
                column: "ClassId");
        }
    }
}
