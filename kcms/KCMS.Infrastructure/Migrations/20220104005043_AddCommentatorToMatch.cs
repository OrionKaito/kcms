using Microsoft.EntityFrameworkCore.Migrations;

namespace KCMS.Infrastructure.Migrations
{
    public partial class AddCommentatorToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commentator",
                table: "Matchs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commentator",
                table: "Matchs");
        }
    }
}
