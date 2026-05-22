using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zumra.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Facilities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_UserID",
                table: "Facilities",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_AspNetUsers_UserID",
                table: "Facilities",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_AspNetUsers_UserID",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_UserID",
                table: "Facilities");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
