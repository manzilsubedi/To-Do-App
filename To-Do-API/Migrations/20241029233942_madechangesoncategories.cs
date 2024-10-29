using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_API.Migrations
{
    /// <inheritdoc />
    public partial class madechangesoncategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ToDos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "ToDos");
        }
    }
}
