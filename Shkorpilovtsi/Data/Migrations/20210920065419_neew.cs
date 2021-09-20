using Microsoft.EntityFrameworkCore.Migrations;

namespace Shkorpilovtsi.Data.Migrations
{
    public partial class neew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Bungalows");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CategoryId",
                table: "Prices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ShiftId",
                table: "Prices",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Categories_CategoryId",
                table: "Prices",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Shifts_ShiftId",
                table: "Prices",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Categories_CategoryId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Shifts_ShiftId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_CategoryId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ShiftId",
                table: "Prices");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Bungalows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
