//using AutoMapper;
//using BudgetApp.Core.Common.DTOs;
//using BudgetApp.Core.Common.Interfaces;
//using BudgetApp.Domain.Entities;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BudgetApp.Core.Budgets.Queries;
//public class GetBudgetQuery : IRequest<BudgetDTO?>
//{
//    public User User { get; set; }
//}

//public class GetBudgetQueryHandler : IRequestHandler<GetBudgetQuery, BudgetDTO?>
//{
//    private readonly IDatabaseService _databaseService;
//    private readonly IMapper _mapper;

//    public GetBudgetQueryHandler(IDatabaseService databaseService)
//    {
//        _databaseService = databaseService;
//    }

//    public async Task<BudgetDTO?> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
//    {

//        UserDTO userDTO = new UserDTO();
//        if (currentUser != null)
//        {
//            userDTO = _mapper.Map<UserDTO>(currentUser);


//            since mapping isn't working, I'll do it myself...
//            userDTO = new UserDTO
//            {
//                Email = currentUser.Email,
//                Password = currentUser.Password,
//                Person = new PersonDTO
//                {
//                    FirstName = currentUser.Person.FirstName,
//                    LastName = currentUser.Person.LastName,
//                    Address = new AddressDTO
//                    {
//                        Street = currentUser.Person.Address.Street,
//                        City = currentUser.Person.Address.City,
//                        State = currentUser.Person.Address.State,
//                        ZipCode = currentUser.Person.Address.ZipCode,
//                    }
//                }
//            };
//        }

//        return userDTO;
//    }

//}