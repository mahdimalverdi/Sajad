using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class addImpossible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsImpossible",
                table: "Answers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImpossible",
                table: "Answers");
        }
    }
}
