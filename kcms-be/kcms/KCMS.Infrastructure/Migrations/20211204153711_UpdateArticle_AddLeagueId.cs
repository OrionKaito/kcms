using Microsoft.EntityFrameworkCore.Migrations;

namespace KCMS.Infrastructure.Migrations
{
    public partial class UpdateArticle_AddLeagueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LeagueId",
                table: "Articles",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Articles");
        }
    }
}
