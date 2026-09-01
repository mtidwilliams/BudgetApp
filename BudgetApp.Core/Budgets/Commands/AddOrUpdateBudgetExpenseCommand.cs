using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Core.Budgets.Commands;
public class AddOrUpdateBudgetExpenseCommand : IRequest<bool>
{
    public required User User { get; set; }
    public Guid? ExpenseId { get; set; }
    public string ExpenseName { get; set; } = string.Empty;
    public decimal ExpenseAmount { get; set; }
}

public class AddOrUpdateBudgetExpenseCommandHandler : IRequestHandler<AddOrUpdateBudgetExpenseCommand, bool>
{
    private readonly IDatabaseService _databaseService;

    public AddOrUpdateBudgetExpenseCommandHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> Handle(AddOrUpdateBudgetExpenseCommand command, CancellationToken cancellationToken)
    {
        if (command.User.Budget == null)
        {
            // Create a budget if this is the first thing we're adding
            command.User.Budget = new Budget();
        }

        var expense = command.User.Budget.Expenses?.SingleOrDefault(x => x.ExpenseId == command.ExpenseId);

        if (expense == default)
        {
            command.User.Budget.Expenses?.Add(new Expense { Name = command.ExpenseName, Amount = command.ExpenseAmount });
        }
        else
        {
            expense.Name = command.ExpenseName;
            expense.Amount = command.ExpenseAmount;
        }

        await _databaseService.SaveChangesAsync(cancellationToken);

        return true;
    }

}