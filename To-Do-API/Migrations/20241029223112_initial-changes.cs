using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_API.Migrations
{
    /// <inheritdoc />
    public partial class initialchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ToDos");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ToDos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDos");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ToDos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
