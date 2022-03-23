using AutoMapper;
using HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;
using HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;
using HallOfFame.Domain.Entities;

namespace HallOfFame.BusinessLogic.Common;

public class BusinessLogicMappingProfile : Profile
{
    public BusinessLogicMappingProfile()
    {
        CreateMap<CreatePersonCommand, Person>();
        CreateMap<UpdatePersonCommand, Person>();
    }
}