using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class addUserIdToAnsAndQues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "QuestionStructs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Answers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuestionStructs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Answers");
        }
    }
}
