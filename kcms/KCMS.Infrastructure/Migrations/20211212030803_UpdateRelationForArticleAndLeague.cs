using Microsoft.EntityFrameworkCore.Migrations;

namespace KCMS.Infrastructure.Migrations
{
    public partial class UpdateRelationForArticleAndLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_LeagueId",
                table: "Articles",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Leagues_LeagueId",
                table: "Articles",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Leagues_LeagueId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_LeagueId",
                table: "Articles");
        }
    }
}
