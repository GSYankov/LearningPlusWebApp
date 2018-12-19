using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningPlus.Web.Migrations
{
    public partial class SutdentsClassesII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ClassesStudents",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesStudents", x => new { x.ClassId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_ClassesStudents_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassesStudents_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassesStudents_StudentId",
                table: "ClassesStudents",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassesStudents");

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
    }
}
