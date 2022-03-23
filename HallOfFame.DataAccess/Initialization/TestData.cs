using HallOfFame.DataAccess.Models;

namespace HallOfFame.DataAccess.Initialization;

public static class TestData
{
    public static List<PersonModel> Persons => new()
    {
        new PersonModel { Id = 1, Name = "August Haynes", DisplayName = "A_Haynes" },
        new PersonModel { Id = 2, Name = "Javier Hardin", DisplayName = "J_Hardin" },
        new PersonModel { Id = 3, Name = "Kristopher Black", DisplayName = "K_Black" }
    };

    public static List<SkillModel> Skills => new()
    {
        new SkillModel { PersonId = 1, Name = "C#", Level = 9 },
        new SkillModel { PersonId = 1, Name = ".NET", Level = 8 },
        new SkillModel { PersonId = 1, Name = "EF Core", Level = 6 },
        new SkillModel { PersonId = 1, Name = "Git", Level = 5 },
        new SkillModel { PersonId = 2, Name = "C#", Level = 7 },
        new SkillModel { PersonId = 2, Name = "EF Core", Level = 5 },
        new SkillModel { PersonId = 2, Name = "WinForms", Level = 3 },
        new SkillModel { PersonId = 3, Name = "C#", Level = 5 },
        new SkillModel { PersonId = 3, Name = ".NET", Level = 4 }
    };
}