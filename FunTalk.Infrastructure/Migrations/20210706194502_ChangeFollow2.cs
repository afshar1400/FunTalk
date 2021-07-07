using Microsoft.EntityFrameworkCore.Migrations;

namespace FunTalk.Infrastructure.Migrations
{
    public partial class ChangeFollow2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Follows",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Follows");
        }
    }
}
