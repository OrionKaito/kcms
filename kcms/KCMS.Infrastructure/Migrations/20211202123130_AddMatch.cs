using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace KCMS.Infrastructure.Migrations
{
    public partial class AddMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true),
                    MatchType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    HomePoints = table.Column<float>(nullable: false),
                    GuestPoints = table.Column<float>(nullable: false),
                    HomeTeamId = table.Column<long>(nullable: false),
                    GuestTeamId = table.Column<long>(nullable: false),
                    LeagueId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matchs_Teams_GuestTeamId",
                        column: x => x.GuestTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchs_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchs_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_GuestTeamId",
                table: "Matchs",
                column: "GuestTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_HomeTeamId",
                table: "Matchs",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_LeagueId",
                table: "Matchs",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matchs");
        }
    }
}
