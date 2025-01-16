
using BudgetApp.Core.Common.Interfaces;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Core.Users.Commands;
public class RegisterUserCommand : IRequest<bool>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Address? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly IDatabaseService _databaseService;

    public RegisterUserCommandHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var person = new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address
        };

        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            Person = person,
            Budget = new Budget()
        };

        await _databaseService.Persons.AddAsync(person);
        await _databaseService.Users.AddAsync(user);

        var result = await _databaseService.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
        
}