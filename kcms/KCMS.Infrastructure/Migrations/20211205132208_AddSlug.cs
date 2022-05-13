using Microsoft.EntityFrameworkCore.Migrations;

namespace KCMS.Infrastructure.Migrations
{
    public partial class AddSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Matchs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Advertisings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Matchs");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Advertisings",
                type: "text",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
