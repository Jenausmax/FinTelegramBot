using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinBot.DB.Migrations
{
    public partial class StickerAndLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogBots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TypeMessage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogBots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FileidTelegram = table.Column<string>(type: "TEXT", nullable: true),
                    StickerRole = table.Column<int>(type: "INTEGER", nullable: true),
                    IsAnimated = table.Column<bool>(type: "INTEGER", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStickers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogBots_Time",
                table: "LogBots",
                column: "Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogBots");

            migrationBuilder.DropTable(
                name: "TelegramStickers");
        }
    }
}
