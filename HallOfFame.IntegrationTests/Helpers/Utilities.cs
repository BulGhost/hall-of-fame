using HallOfFame.DataAccess.DbContext;
using HallOfFame.DataAccess.Initialization;

namespace HallOfFame.IntegrationTests.Helpers;

public static class Utilities
{
    public static void InitializeDbForTests(HallOfFameDbContext db)
    {
        db.Persons.AddRange(TestData.Persons);
        db.Skills.AddRange(TestData.Skills);
        db.SaveChanges();
    }
}