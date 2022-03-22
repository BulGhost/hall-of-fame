using System.Reflection;
using HallOfFame.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DataAccess.DbContext;

public class HallOfFameDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public HallOfFameDbContext(DbContextOptions<HallOfFameDbContext> options) : base(options)
    {
    }

    public DbSet<PersonModel> Persons { get; set; }
    public DbSet<SkillModel> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}