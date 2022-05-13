using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace KCMS.Infrastructure.Migrations
{
    public partial class AddTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rankings");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(maxLength: 100, nullable: true),
                    Image = table.Column<string>(nullable: true),
                    LeagueId = table.Column<long>(nullable: false),
                    ST = table.Column<int>(nullable: false),
                    T = table.Column<int>(nullable: false),
                    H = table.Column<int>(nullable: false),
                    B = table.Column<int>(nullable: false),
                    TG = table.Column<int>(nullable: false),
                    TH = table.Column<int>(nullable: false),
                    HS = table.Column<int>(nullable: false),
                    D = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Articles");

            migrationBuilder.CreateTable(
                name: "Rankings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    B = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    D = table.Column<int>(type: "int", nullable: false),
                    H = table.Column<int>(type: "int", nullable: false),
                    HS = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ST = table.Column<int>(type: "int", nullable: false),
                    T = table.Column<int>(type: "int", nullable: false),
                    TG = table.Column<int>(type: "int", nullable: false),
                    TH = table.Column<int>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rankings", x => x.Id);
                });
        }
    }
}
