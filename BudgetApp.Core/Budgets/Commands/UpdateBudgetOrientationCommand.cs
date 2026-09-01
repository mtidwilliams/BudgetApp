using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Core.Budgets.Commands;
public class UpdateBudgetOrientationCommand : IRequest<bool>
{
    public required User User { get; set; }
    public required Budget Budget { get; set; }
}

public class UpdateBudgetOrientationCommandHandler : IRequestHandler<UpdateBudgetOrientationCommand, bool>
{
    private readonly IDatabaseService _databaseService;

    public UpdateBudgetOrientationCommandHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> Handle(UpdateBudgetOrientationCommand command, CancellationToken cancellationToken)
    {
        command.User.Budget = command.Budget;
        await _databaseService.SaveChangesAsync(cancellationToken);
        return true;
    }
}