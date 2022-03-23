using HallOfFame.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace HallOfFame.DataAccess.Initialization;

public static class TestDataInitializer
{
    public static async Task Initialize(HallOfFameDbContext context)
    {
        await DropAndCreateDatabase(context);
        await SeedData(context);
    }

    internal static async Task DropAndCreateDatabase(HallOfFameDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    internal static async Task SeedData(HallOfFameDbContext context)
    {
        try
        {
            await ProcessInsert(context, context.Persons, TestData.Persons);
            await ProcessInsert(context, context.Skills, TestData.Skills, false);
        }
        catch (Exception)
        {
            await context.DisposeAsync();
            throw;
        }
    }

    private static async Task ProcessInsert<TEntity>(HallOfFameDbContext context, DbSet<TEntity> table,
        List<TEntity> records, bool hasIdentityKey = true) where TEntity : class
    {
        if (await table.AnyAsync()) return;

        IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
            try
            {
                IEntityType metaData = context.Model.FindEntityType(typeof(TEntity).FullName!);
                string schemaName = metaData!.GetSchema() ?? "dbo";
                string tableName = metaData.GetTableName();
                if (hasIdentityKey)
                    await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {schemaName}.{tableName} ON");
                await table.AddRangeAsync(records);
                await context.SaveChangesAsync();
                if (hasIdentityKey)
                    await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {schemaName}.{tableName} OFF");
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }
}