using HallOfFame.BusinessLogic;
using HallOfFame.BusinessLogic.Common;
using HallOfFame.DataAccess;
using HallOfFame.DataAccess.DbContext;
using HallOfFame.DataAccess.Initialization;
using HallOfFame.DataAccess.Models;
using HallOfFame.WebApi.HelperClasses;
using HallOfFame.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using ILogger = NLog.ILogger;

Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddBusinessLogic();
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDataAccess(connection);
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.AddAutoMapper(typeof(BusinessLogicMappingProfile), typeof(DataAccessMappingProfile));
    builder.Services.AddCorsPolicy();
    builder.Services.AddControllers();
    builder.Services.Configure<RouteOptions>(opt => opt.LowercaseUrls = true);
    builder.Services.AddCustomApiVersioning();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionsConfigurator>();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }

    app.UseCustomExceptionHandler();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseApiVersioning();
    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        await InitializeTestData(app.Services, logger);
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

static async Task InitializeTestData(IServiceProvider serviceProvider, ILogger logger)
{
    using IServiceScope scope = serviceProvider.CreateScope();
    IServiceProvider services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<HallOfFameDbContext>();
        await TestDataInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        logger.Fatal(ex, "An error occured while app initialization");
    }
}

public partial class Program { }