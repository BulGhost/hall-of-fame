using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HallOfFame.DataAccess.DbContext;

public class HallOfFameDbContextFactory : IDesignTimeDbContextFactory<HallOfFameDbContext>
{
    public HallOfFameDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HallOfFameDbContext>();
        const string connectionString = @"Server=.;Database=HallOfFame;Trusted_Connection=True";
        optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        return new HallOfFameDbContext(optionsBuilder.Options);
    }
}