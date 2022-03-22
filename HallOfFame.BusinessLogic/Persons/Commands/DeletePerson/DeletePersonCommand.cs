using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.DeletePerson;

public record DeletePersonCommand(long PersonId) : IRequest<long>;