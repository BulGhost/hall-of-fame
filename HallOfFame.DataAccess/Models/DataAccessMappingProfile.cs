using AutoMapper;
using HallOfFame.Domain.Entities;

namespace HallOfFame.DataAccess.Models;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<Person, PersonModel>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(person =>
                person.Skills.Select(skill => new SkillModel { PersonId = person.Id, Name = skill.Name, Level = skill.Level })))
            .ReverseMap();

        CreateMap<Skill, SkillModel>()
            .ForMember(nameof(SkillModel.PersonId), opt => opt.Ignore())
            .ReverseMap();

        CreateMap<PersonModel, PersonModel>();
    }
}