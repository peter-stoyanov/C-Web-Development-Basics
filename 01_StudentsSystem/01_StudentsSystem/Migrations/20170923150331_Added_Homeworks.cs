using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudentsSystem.Migrations
{
    public partial class Added_Homeworks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Homework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Homework", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Homework_tbl_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Homework_tbl_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tbl_Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Homework_CourseId",
                table: "tbl_Homework",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Homework_StudentId",
                table: "tbl_Homework",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Homework");
        }
    }
}