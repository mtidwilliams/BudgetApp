using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expense_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budget",
                        principalColumn: "BudgetId");
                });

            migrationBuilder.CreateTable(
                name: "IncomeSource",
                columns: table => new
                {
                    IncomeSourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeSource", x => x.IncomeSourceId);
                    table.ForeignKey(
                        name: "FK_IncomeSource_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budget",
                        principalColumn: "BudgetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BudgetId",
                table: "Users",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_BudgetId",
                table: "Expense",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeSource_BudgetId",
                table: "IncomeSource",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Budget_BudgetId",
                table: "Users",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Budget_BudgetId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "IncomeSource");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropIndex(
                name: "IX_Users_BudgetId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "Users");
        }
    }
}
