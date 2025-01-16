using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Budget_BudgetId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeSource_Budget_BudgetId",
                table: "IncomeSource");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Budget_BudgetId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeSource",
                table: "IncomeSource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expense",
                table: "Expense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Budget",
                table: "Budget");

            migrationBuilder.RenameTable(
                name: "IncomeSource",
                newName: "IncomeSources");

            migrationBuilder.RenameTable(
                name: "Expense",
                newName: "Expenses");

            migrationBuilder.RenameTable(
                name: "Budget",
                newName: "Budgets");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeSource_BudgetId",
                table: "IncomeSources",
                newName: "IX_IncomeSources_BudgetId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_BudgetId",
                table: "Expenses",
                newName: "IX_Expenses_BudgetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeSources",
                table: "IncomeSources",
                column: "IncomeSourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "ExpenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budgets",
                table: "Budgets",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeSources_Budgets_BudgetId",
                table: "IncomeSources",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Budgets_BudgetId",
                table: "Users",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeSources_Budgets_BudgetId",
                table: "IncomeSources");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Budgets_BudgetId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeSources",
                table: "IncomeSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Budgets",
                table: "Budgets");

            migrationBuilder.RenameTable(
                name: "IncomeSources",
                newName: "IncomeSource");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "Expense");

            migrationBuilder.RenameTable(
                name: "Budgets",
                newName: "Budget");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeSources_BudgetId",
                table: "IncomeSource",
                newName: "IX_IncomeSource_BudgetId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_BudgetId",
                table: "Expense",
                newName: "IX_Expense_BudgetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeSource",
                table: "IncomeSource",
                column: "IncomeSourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expense",
                table: "Expense",
                column: "ExpenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budget",
                table: "Budget",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Budget_BudgetId",
                table: "Expense",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeSource_Budget_BudgetId",
                table: "IncomeSource",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Budget_BudgetId",
                table: "Users",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
