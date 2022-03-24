using System;
using System.Linq;
using HallOfFame.DataAccess.DbContext;
using HallOfFame.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HallOfFame.IntegrationTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<HallOfFameDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<HallOfFameDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDbForTesting"));

            ServiceProvider sp = services.BuildServiceProvider();

            using IServiceScope scope = sp.CreateScope();
            IServiceProvider scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<HallOfFameDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try
            {
                Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database with test messages. " +
                                    "Error: {Message}", ex.Message);
            }
        });
    }

    protected override IHostBuilder CreateHostBuilder() =>
        base.CreateHostBuilder().ConfigureHostConfiguration(config =>
            config.AddEnvironmentVariables("ASPNETCORE"));
}