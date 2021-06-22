using Microsoft.EntityFrameworkCore.Migrations;

namespace FinBot.DB.Migrations
{
    public partial class IndexCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Incomes_Date",
                table: "Incomes",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_Date",
                table: "Consumptions",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Incomes_Date",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Consumptions_Date",
                table: "Consumptions");
        }
    }
}
