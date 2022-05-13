using Microsoft.EntityFrameworkCore.Migrations;

namespace KCMS.Infrastructure.Migrations
{
    public partial class UpdateLeague_AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Leagues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Leagues");
        }
    }
}
