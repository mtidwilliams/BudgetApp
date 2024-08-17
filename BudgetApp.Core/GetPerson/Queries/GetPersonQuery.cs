
using BudgetApp.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;

namespace BudgetApp.Core.GetPerson.Queries;
public class GetPersonQuery : IRequest<PersonDTO>
{
    public string Email { get; set; }
}

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDTO>
{
    private readonly IDatabaseService _databaseService;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<PersonDTO> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _databaseService.Persons.FirstOrDefaultAsync(x => x.User.Email == request.Email, cancellationToken);

        var newPerson = _mapper.Map<PersonDTO>(person);

        return newPerson;
    }
        
}