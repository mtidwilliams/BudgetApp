
using BudgetApp.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Core.Users.Queries;
public class GetUserQuery : IRequest<User>
{
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly IDatabaseService _databaseService;
    //private readonly IMapper _mapper;

    public GetUserQueryHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User? currentUser = request.UserId != null
            ? await _databaseService.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken)
            : await _databaseService.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        // UserDTO userDTO = new UserDTO();
        // if(currentUser != null)
        // {
        //     // userDTO = _mapper.Map<UserDTO>(currentUser);

        //     //since mapping isn't working, I'll do it myself...
        //     userDTO = new UserDTO
        //     {
        //         UserId = currentUser.UserId,
        //         Email = currentUser.Email,
        //         Password = currentUser.Password,
        //         Person = new PersonDTO
        //         {
        //             PersonId = currentUser.PersonId,
        //             FirstName = currentUser.Person.FirstName,
        //             LastName = currentUser.Person.LastName,
        //             Address = new AddressDTO
        //             {
        //                 Street = currentUser.Person.Address.Street,
        //                 City = currentUser.Person.Address.City,
        //                 State = currentUser.Person.Address.State,
        //                 ZipCode = currentUser.Person.Address.ZipCode,
        //             }
        //         },
        //         Budget = new BudgetDTO
        //         {
        //             BudgetId = currentUser.Budget?.BudgetId ?? Guid.Empty,
        //             Expenses = currentUser.Budget?.Expenses?.Select(x => new ExpenseDTO { ExpenseId = x.ExpenseId, Amount = x.Amount, Name = x.Name }).ToList(),
        //             IncomeSources = currentUser.Budget?.IncomeSources?.Select(x => new IncomeSourceDTO { IncomeSourceId = x.IncomeSourceId, Amount = x.Amount, Name = x.Name }).ToList(),
        //         }
        //     };
        // }

        return currentUser;
    }
        
}