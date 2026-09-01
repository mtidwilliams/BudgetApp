
using BudgetApp.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;

namespace BudgetApp.Core.GetPerson.Queries;
public class LoginUserCommand : IRequest<bool>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
{
    private readonly IDatabaseService _databaseService;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IDatabaseService databaseService, IMapper mapper)
    {
        _databaseService = databaseService;
        _mapper = mapper;
    }

    public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _databaseService.Users.AnyAsync(x => x.Email == request.Email && x.Password == request.Password, cancellationToken);

        // var mappedUser = _mapper.Map<UserDTO>(user);

        return user;
    }
        
}