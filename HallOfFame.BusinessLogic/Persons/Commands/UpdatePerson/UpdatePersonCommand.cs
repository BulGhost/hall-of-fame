using HallOfFame.Domain.Entities;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;

public record UpdatePersonCommand(long Id, string Name, string DisplayName, List<Skill> Skills) : IRequest;