using Microsoft.EntityFrameworkCore.Migrations;

namespace FunTalk.Infrastructure.Migrations
{
    public partial class AddCmtCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CmtCount",
                table: "Jokes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CmtCount",
                table: "Jokes");
        }
    }
}
