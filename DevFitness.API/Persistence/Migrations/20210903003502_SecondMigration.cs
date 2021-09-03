using Microsoft.EntityFrameworkCore.Migrations;

namespace DevFitness.API.Persistence.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_UserId1",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_UserId1",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Meals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Meals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId1",
                table: "Meals",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_UserId1",
                table: "Meals",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
