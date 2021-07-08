using Microsoft.EntityFrameworkCore.Migrations;

namespace FinBot.DB.Migrations
{
    public partial class addFileidUniqueSticker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileidUnique",
                table: "TelegramStickers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileidUnique",
                table: "TelegramStickers");
        }
    }
}
