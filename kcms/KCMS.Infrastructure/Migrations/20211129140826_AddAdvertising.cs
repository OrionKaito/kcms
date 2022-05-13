using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace KCMS.Infrastructure.Migrations
{
    public partial class AddAdvertising : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Options = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_Position",
                table: "Advertisings",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_Url",
                table: "Advertisings",
                column: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisings");
        }
    }
}
