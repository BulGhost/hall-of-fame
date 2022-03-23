namespace HallOfFame.DataAccess.Models;

public class PersonModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<SkillModel> Skills { get; set; }
}