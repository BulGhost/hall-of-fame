using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.WebApi.HelperClasses;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(opt =>
        {
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder
                .WithOrigins("https://localhost:5001")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()));

        return services;
    }
}