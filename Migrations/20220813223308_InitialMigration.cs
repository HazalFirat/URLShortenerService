using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URLShortenerService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URLs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginalURL = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    ShortURL = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_URLs_OriginalURL_ShortURL",
                table: "URLs",
                columns: new[] { "OriginalURL", "ShortURL" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_URLs_ShortURL",
                table: "URLs",
                column: "ShortURL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "URLs");
        }
    }
}
