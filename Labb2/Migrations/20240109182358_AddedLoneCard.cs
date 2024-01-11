using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class AddedLoneCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoneCard",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoneCard",
                table: "User");
        }
    }
}
