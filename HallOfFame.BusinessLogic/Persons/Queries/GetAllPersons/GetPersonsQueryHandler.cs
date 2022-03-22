using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Queries.GetAllPersons;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, IEnumerable<Person>>
{
    private readonly IPersonRepo _repo;

    public GetPersonsQueryHandler(IPersonRepo repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Person>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync(cancellationToken);
    }
}