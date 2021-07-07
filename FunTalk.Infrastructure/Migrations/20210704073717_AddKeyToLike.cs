using Microsoft.EntityFrameworkCore.Migrations;

namespace FunTalk.Infrastructure.Migrations
{
    public partial class AddKeyToLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Likes",
                newName: "LikeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikeId",
                table: "Likes",
                newName: "Id");
        }
    }
}
