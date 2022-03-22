using AutoMapper;
using HallOfFame.BusinessLogic.Common.Mappings;
using HallOfFame.Domain.Entities;
using MediatR;

namespace HallOfFame.BusinessLogic.Person.Commands.CreatePerson;

public record CreatePersonCommand(string Name, string DisplayName, List<Skill> Skills)
    : IRequest<long>, IMappable
{
    public void Mapping(Profile profile) => profile.CreateMap<CreatePersonCommand, Domain.Entities.Person>();
}