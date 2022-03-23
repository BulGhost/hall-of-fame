using AutoMapper;
using HallOfFame.BusinessLogic.Common.Mappings;
using HallOfFame.Domain.Entities;
using MediatR;

namespace HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;

public record UpdatePersonCommand(long Id, string Name, string DisplayName, List<Skill> Skills)
    : IRequest, IMappable
{
    public void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(Person));
}