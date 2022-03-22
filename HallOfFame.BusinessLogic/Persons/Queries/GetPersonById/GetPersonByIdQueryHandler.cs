using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Queries.GetPersonById;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person>
{
    private readonly IPersonRepo _repo;

    public GetPersonByIdQueryHandler(IPersonRepo repo)
    {
        _repo = repo;
    }

    public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.FindAsync(request.PersonId, cancellationToken);
    }
}