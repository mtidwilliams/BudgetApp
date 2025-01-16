
using BudgetApp.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;

namespace BudgetApp.Core.Users.Queries;
public class GetUserQuery : IRequest<UserDTO>
{
    public string? Email { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
{
    private readonly IDatabaseService _databaseService;
    //private readonly IMapper _mapper;

    public GetUserQueryHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _databaseService.Users
            .Include(x => x.Person)
            .Include(x => x.Person.Address)
            .Include(x => x.Budget)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        UserDTO userDTO = new UserDTO();
        if(currentUser != null)
        {
            // userDTO = _mapper.Map<UserDTO>(currentUser);

            //since mapping isn't working, I'll do it myself...
            userDTO = new UserDTO
            {
                Email = currentUser.Email,
                Password = currentUser.Password,
                Person = new PersonDTO
                {
                    FirstName = currentUser.Person.FirstName,
                    LastName = currentUser.Person.LastName,
                    Address = new AddressDTO
                    {
                        Street = currentUser.Person.Address.Street,
                        City = currentUser.Person.Address.City,
                        State = currentUser.Person.Address.State,
                        ZipCode = currentUser.Person.Address.ZipCode,
                    }
                },
                Budget = new BudgetDTO
                {
                    Expenses = currentUser.Budget?.Expenses?.Select(x => new ExpenseDTO { Amount = x.Amount, Name = x.Name }).ToList(),
                    IncomeSources = currentUser.Budget?.IncomeSources?.Select(x => new IncomeSourceDTO { Amount = x.Amount, Name = x.Name }).ToList(),
                }
            };
        }

        return userDTO;
    }
        
}