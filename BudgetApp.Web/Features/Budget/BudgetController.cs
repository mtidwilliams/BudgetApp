using BudgetApp.Core.Budgets.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpPost("addExpense")]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseModel model)
    {
        if (!ModelState.IsValid)
        {
            return Conflict(new { message = "Could not add expense. Please check your entries and try again." });
        }

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var added = await _mediator.Send(new AddBudgetExpenseCommand {UserId = userId, ExpenseName = model.ExpenseName, ExpenseAmount = model.ExpenseAmount });

        if(!added)
        {
            return Conflict(new { message = "An error occurred adding your expense. Please try again." });
        }

        return Ok();
    }
}
