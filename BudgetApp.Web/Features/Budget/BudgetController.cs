using BudgetApp.Core.Budgets.Commands;
using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Core.Users.Queries;
using BudgetApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace BudgetApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BudgetController : Controller
{
    private readonly IMediator _mediator;
    public BudgetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private async Task<User> GetUser()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        return await _mediator.Send(new GetUserQuery { UserId = userId });
    }

    [HttpPost("getCurrentBudget")]
    public async Task<Budget?> GetCurrentBudget()
    {
        var budget = (await GetUser()).Budget;
        if(budget != null) {
            var orderedExpenses = budget.Expenses?.OrderBy(x => x.SortOrder).ToList();
            var orderedIncomeSources = budget.IncomeSources?.OrderBy(x => x.SortOrder).ToList();
            budget.Expenses = orderedExpenses;
            budget.IncomeSources = orderedIncomeSources;
        }
        return budget;
    }

    [HttpPost("addExpense")]
    public async Task<IActionResult> AddExpense([FromBody] AddOrUpdateExpenseModel model)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not add expense. Please check your entries and try again." });
        }

        var user = await GetUser();
        var added = await _mediator.Send(new AddOrUpdateBudgetExpenseCommand { User = user, ExpenseName = model.ExpenseName, ExpenseAmount = model.ExpenseAmount });

        if(!added)
        {
            return Conflict(new { message = "An error occurred adding your expense. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }

    [HttpPost("updateExpense")]
    public async Task<IActionResult> UpdateExpense([FromBody] AddOrUpdateExpenseModel model)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not update expense. Please check your entries and try again." });
        }

        var user = await GetUser();
        var updated = await _mediator.Send(new AddOrUpdateBudgetExpenseCommand { User = user, ExpenseId = model.ExpenseId, ExpenseAmount = model.ExpenseAmount, ExpenseName = model.ExpenseName });

        if(!updated)
        {
            return Conflict(new { message = "An error occurred updating your expense. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }

    [HttpPost("removeExpense")]
    public async Task<IActionResult> RemoveExpense([FromQuery] string expenseId)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not remove expense. Please check your entries and try again." });
        }

        var user = await GetUser();
        var removed = await _mediator.Send(new DeleteExpenseCommand { User = user, ExpenseId = Guid.Parse(expenseId) });

        if(!removed)
        {
            return Conflict(new { message = "An error occurred deleting your expense. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }

    [HttpPost("addIncomeSource")]
    public async Task<IActionResult> AddIncomeSource([FromBody] AddOrUpdateIncomeSourceModel model)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not add income source. Please check your entries and try again." });
        }

        var user = await GetUser();
        var added = await _mediator.Send(new AddOrUpdateBudgetIncomeSourceCommand { User = user, IncomeSourceName = model.IncomeSourceName, IncomeSourceAmount = model.IncomeSourceAmount });

        if(!added)
        {
            return Conflict(new { message = "An error occurred adding your income source. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }

    [HttpPost("updateIncomeSource")]
    public async Task<IActionResult> UpdateIncomeSource([FromBody] AddOrUpdateIncomeSourceModel model)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not update income source. Please check your entries and try again." });
        }

        var user = await GetUser();
        var updated = await _mediator.Send(new AddOrUpdateBudgetIncomeSourceCommand { User = user, IncomeSourceId = model.IncomeSourceId, IncomeSourceName = model.IncomeSourceName, IncomeSourceAmount = model.IncomeSourceAmount });

        if(!updated)
        {
            return Conflict(new { message = "An error occurred updating your income source. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }

    [HttpPost("removeIncomeSource")]
    public async Task<IActionResult> RemoveIncomeSource([FromQuery] string incomeSourceId)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not remove income source. Please check your entries and try again." });
        }

        var user = await GetUser();
        var removed = await _mediator.Send(new DeleteIncomeSourceCommand { User = user, IncomeSourceId = Guid.Parse(incomeSourceId) });

        if(!removed)
        {
            return Conflict(new { message = "An error occurred deleting your income source. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }

    [HttpPost("saveBudgetOrientation")]
    public async Task<IActionResult> SaveBudgetOrientation([FromBody] SaveBudgetOrientationModel model)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not update your budget's orientation. Please check your entries and try again." });
        }

        var user = await GetUser();
        var saved = await _mediator.Send(new UpdateBudgetOrientationCommand { User = user, Budget = model.Budget });

        if(!saved)
        {
            return Conflict(new { message = "An error occurred updating your budget's orientation. Please try again." });
        }

        return Ok(new { Budget = user.Budget });
    }
}
