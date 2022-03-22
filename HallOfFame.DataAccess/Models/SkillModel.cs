using AutoMapper;
using HallOfFame.BusinessLogic.Common.Mappings;
using HallOfFame.Domain.Entities;

namespace HallOfFame.DataAccess.Models;

public class SkillModel : IMappable
{
    public long PersonId { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(Skill), GetType())
            .ForMember(nameof(PersonId), opt => opt.Ignore())
            .ReverseMap();
    }
}