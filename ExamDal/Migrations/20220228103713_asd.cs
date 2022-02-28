using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamDal.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamDefId",
                table: "Exams",
                column: "ExamDefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamDefinitions_ExamDefId",
                table: "Exams",
                column: "ExamDefId",
                principalTable: "ExamDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamDefinitions_ExamDefId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ExamDefId",
                table: "Exams");
        }
    }
}
