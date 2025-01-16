using BudgetApp.Core.Budgets.Commands;
using BudgetApp.Core.GetPerson.Queries;
using BudgetApp.Core.Users.Commands;
using BudgetApp.Core.Users.Queries;
using BudgetApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;

    public AuthController(ITokenService tokenService, IMediator mediator)
    {
        _tokenService = tokenService;
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model is null)
        {
            return BadRequest("Invalid client request");
        }

        var allowedToLogin = await _mediator.Send(new LoginUserCommand { Email = model.Email, Password = model.Password });

        if (allowedToLogin)
        {
            var user = (await _mediator.Send(new GetUserQuery { Email = model.Email }));
            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token, User = user });
        }

        return Conflict(new { message = "Unable to find a user with this email address or password. Please try again." });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountModel model)
    {
        if (model is null || !ModelState.IsValid)
        {
            return BadRequest("Invalid client request");
        }

        var userAlreadyExists = (await _mediator.Send(new GetUserQuery { Email = model.Email })).UserId != Guid.Empty;
        if (userAlreadyExists)
        {
            return Conflict(new { message = "A user with this email address already exists." });
        }

        var address = new Address
        {
            Street = model.Street,
            City = model.City,
            State = model.State,
            ZipCode = model.Zip
        };

        var registered = await _mediator.Send(new RegisterUserCommand
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = address,
            Email = model.Email,
            Password = model.Password
        });

        if(!registered)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request. Please try again." });
        }

        return Ok();
    }
}
