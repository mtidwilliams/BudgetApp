
using BudgetApp.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;

namespace BudgetApp.Core.GetPerson.Queries;
public class LoginUserCommand : IRequest<bool>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
{
    private readonly IDatabaseService _databaseService;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _databaseService.Users.AnyAsync(x => x.Email == request.Email && x.Password == request.Password, cancellationToken);

        // var mappedUser = _mapper.Map<UserDTO>(user);

        return user;
    }
        
}