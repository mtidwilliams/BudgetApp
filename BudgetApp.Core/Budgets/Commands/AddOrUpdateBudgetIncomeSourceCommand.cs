using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Core.Budgets.Commands;
public class AddOrUpdateBudgetIncomeSourceCommand : IRequest<bool>
{
    public required User User { get; set; }
    public Guid? IncomeSourceId { get; set; }
    public string IncomeSourceName { get; set; } = string.Empty;
    public decimal IncomeSourceAmount { get; set; }
}

public class AddOrUpdateBudgetIncomeSourceCommandHandler : IRequestHandler<AddOrUpdateBudgetIncomeSourceCommand, bool>
{
    private readonly IDatabaseService _databaseService;

    public AddOrUpdateBudgetIncomeSourceCommandHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> Handle(AddOrUpdateBudgetIncomeSourceCommand command, CancellationToken cancellationToken)
    {
        if (command.User.Budget == null)
        {
            // Create a budget if this is the first thing we're adding
            command.User.Budget = new Budget();
        }

        var incomeSource = command.User.Budget.IncomeSources?.SingleOrDefault(x => x.IncomeSourceId == command.IncomeSourceId);

        if (incomeSource == default)
        {
            command.User.Budget.IncomeSources?.Add(new IncomeSource { Name = command.IncomeSourceName, Amount = command.IncomeSourceAmount });
        }
        else
        {
            incomeSource.Name = command.IncomeSourceName;
            incomeSource.Amount = command.IncomeSourceAmount;
        }

        await _databaseService.SaveChangesAsync(cancellationToken);

        return true;
    }

}