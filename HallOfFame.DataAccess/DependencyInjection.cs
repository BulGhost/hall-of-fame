using HallOfFame.DataAccess.DbContext;
using HallOfFame.DataAccess.Repositories;
using HallOfFame.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HallOfFameDbContext>(options =>
            options.UseSqlServer(connectionString,
                optionsBuilder => optionsBuilder.EnableRetryOnFailure()));

        services.AddScoped<IPersonRepo, PersonRepo>();
        return services;
    }
}