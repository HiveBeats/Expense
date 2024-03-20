using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyOwnerId",
                table: "Attendees",
                type: "character varying(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_FamilyOwnerId",
                table: "Attendees",
                column: "FamilyOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Attendees_FamilyOwnerId",
                table: "Attendees",
                column: "FamilyOwnerId",
                principalTable: "Attendees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Attendees_FamilyOwnerId",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_FamilyOwnerId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "FamilyOwnerId",
                table: "Attendees");
        }
    }
}
