using BudgetApp.Core.Common.DTOs;
using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Core.Budgets.Commands;
public class DeleteIncomeSourceCommand : IRequest<bool>
{
    public required User User { get; set; }
    public Guid? IncomeSourceId { get; set; }
}

public class DeleteIncomeSourceCommandHandler : IRequestHandler<DeleteIncomeSourceCommand, bool>
{
    private readonly IDatabaseService _databaseService;
    private readonly IMediator _mediator;

    public DeleteIncomeSourceCommandHandler(IDatabaseService databaseService, IMediator mediator)
    {
        _databaseService = databaseService;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteIncomeSourceCommand command, CancellationToken cancellationToken)
    {

        if (command.User.Budget == null)
        {
            return false;
        }

        var incomeSource = command.User.Budget.IncomeSources?.SingleOrDefault(x => x.IncomeSourceId == command.IncomeSourceId);

        if (incomeSource == default)
        {
            return false;
        }
        else
        {
            command.User.Budget.IncomeSources!.Remove(incomeSource);
        }

        await _databaseService.SaveChangesAsync(cancellationToken);

        return true;
    }

}