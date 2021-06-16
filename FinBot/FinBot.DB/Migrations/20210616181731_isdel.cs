using Microsoft.EntityFrameworkCore.Migrations;

namespace FinBot.DB.Migrations
{
    public partial class isdel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Incomes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Consumptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Consumptions");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Categories");
        }
    }
}
