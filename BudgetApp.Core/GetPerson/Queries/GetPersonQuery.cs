
using BudgetApp.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BudgetApp.Core.Common.DTOs;
using AutoMapper;

namespace BudgetApp.Core.GetPerson.Queries;
public class GetPersonQuery : IRequest<PersonDTO>
{
    public string Email { get; set; } = string.Empty;
}

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDTO>
{
    private readonly IDatabaseService _databaseService;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(IDatabaseService databaseService, IMapper mapper)
    {
        _databaseService = databaseService;
        _mapper = mapper;
    }

    public async Task<PersonDTO> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var user = await _databaseService.Users.Include(x => x.Person).Include(x => x.Person.Address).FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        var mappedPerson = _mapper.Map<PersonDTO>(user?.Person);

        return mappedPerson;
    }
        
}