using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSortOrderProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "IncomeSources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "IncomeSources");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Expenses");
        }
    }
}
