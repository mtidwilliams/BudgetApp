using BudgetApp.Core.Common.DTOs;
using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Core.Budgets.Commands;
public class DeleteExpenseCommand : IRequest<bool>
{
    public required User User { get; set; }
    public Guid? ExpenseId { get; set; }
}

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
{
    private readonly IDatabaseService _databaseService;
    private readonly IMediator _mediator;

    public DeleteExpenseCommandHandler(IDatabaseService databaseService, IMediator mediator)
    {
        _databaseService = databaseService;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {

        if (command.User.Budget == null)
        {
            return false;
        }

        var expense = command.User.Budget.Expenses?.SingleOrDefault(x => x.ExpenseId == command.ExpenseId);

        if (expense == default)
        {
            return false;
        }
        else
        {
            command.User.Budget.Expenses!.Remove(expense);
        }

        await _databaseService.SaveChangesAsync(cancellationToken);

        return true;
    }

}