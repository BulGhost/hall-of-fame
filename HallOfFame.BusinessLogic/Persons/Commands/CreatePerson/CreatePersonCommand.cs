using HallOfFame.Domain.Entities;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;

public record CreatePersonCommand(string Name, string DisplayName, List<Skill> Skills) : IRequest<long>;