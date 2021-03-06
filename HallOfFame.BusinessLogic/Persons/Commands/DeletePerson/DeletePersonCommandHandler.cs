using HallOfFame.Domain.Repositories;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepo _repo;

    public DeletePersonCommandHandler(IPersonRepo repo)
    {
        _repo = repo;
    }


    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(request.PersonId, cancellationToken);
        return Unit.Value;
    }
}