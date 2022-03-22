using AutoMapper;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Repositories;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, long>
{
    private readonly IPersonRepo _repo;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IPersonRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<long> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);
        await _repo.UpdateAsync(person, cancellationToken);
        return person.Id;
    }
}