using Microsoft.EntityFrameworkCore.Migrations;

namespace FunTalk.Infrastructure.Migrations
{
    public partial class LikeCountAddedToJoke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Jokes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Jokes");
        }
    }
}
