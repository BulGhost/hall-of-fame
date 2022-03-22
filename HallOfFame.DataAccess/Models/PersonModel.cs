using AutoMapper;
using HallOfFame.BusinessLogic.Common.Mappings;
using HallOfFame.Domain.Entities;

namespace HallOfFame.DataAccess.Models;

public class PersonModel : IMappable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<SkillModel> Skills { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Person, PersonModel>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(person =>
                person.Skills.Select(skill => new SkillModel { PersonId = person.Id, Name = skill.Name, Level = skill.Level })))
            .ReverseMap();
    }
}