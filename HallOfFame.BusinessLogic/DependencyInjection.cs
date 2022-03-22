using System.Reflection;
using FluentValidation;
using HallOfFame.BusinessLogic.Common.Behaviors;
using HallOfFame.BusinessLogic.Common.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(config =>
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));

            return services;
        }
    }
}
