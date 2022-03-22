using AutoMapper;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, long>
{
    private readonly IPersonRepo _repo;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IPersonRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<long> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);
        await _repo.AddAsync(person, cancellationToken);
        return person.Id;
    }
}