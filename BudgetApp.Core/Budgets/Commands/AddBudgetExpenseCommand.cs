using AutoMapper;
using BudgetApp.Core.Common.DTOs;
using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Core.Budgets.Commands;
public class AddBudgetExpenseCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string ExpenseName { get; set; }
    public decimal ExpenseAmount { get; set; }
}

public class AddBudgetExpenseCommandHandler : IRequestHandler<AddBudgetExpenseCommand, bool>
{
    private readonly IDatabaseService _databaseService;

    public AddBudgetExpenseCommandHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> Handle(AddBudgetExpenseCommand command, CancellationToken cancellationToken)
    {
        var user = await _databaseService.Users.Include(x => x.Budget).FirstAsync(x => x.UserId == command.UserId, cancellationToken);
        if (user.Budget == null)
        {
            // Create a budget if this is the first thing we're adding
            user.Budget = new Budget();
        }

        user.Budget.Expenses.Add(new Expense { Name = command.ExpenseName, Amount = command.ExpenseAmount });

        await _databaseService.SaveChangesAsync(cancellationToken);

        return true;
    }

}