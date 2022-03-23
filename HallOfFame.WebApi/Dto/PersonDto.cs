using HallOfFame.Domain.Entities;

namespace HallOfFame.WebApi.Dto;

public class PersonDto
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<Skill> Skills { get; set; }
}