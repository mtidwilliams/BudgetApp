using BudgetApp.Core.GetPerson.Queries;
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
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        if (model is null)
        {
            return BadRequest("Invalid client request");
        }

        var allowedToLogin = await _mediator.Send(new LoginUserCommand { Email = model.Email, Password = model.Password });

        if (allowedToLogin)
        {
            var token = _tokenService.GenerateToken(model.Email);
            var person = await _mediator.Send(new GetPersonQuery { Email = model.Email });
            return Ok(new { Token = token, FirstName = person?.FirstName });
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterAccountModel model)
    {
        if (model is null)
        {
            return BadRequest("Invalid client request");
        }

        // var userAlreadyExists = _dbService.Users.Any(x => x.Email == model.Email);
        // if (userAlreadyExists)
        // {
        //     return RedirectToAction(nameof(Register), nameof(AuthController));
        // }

        return Ok();
    }
}
