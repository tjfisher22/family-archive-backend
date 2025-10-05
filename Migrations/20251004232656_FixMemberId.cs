using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyArchiveBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixMemberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Members",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberId",
                table: "Members",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Members_MemberId",
                table: "Members",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Members_MemberId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MemberId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Members");
        }
    }
}
