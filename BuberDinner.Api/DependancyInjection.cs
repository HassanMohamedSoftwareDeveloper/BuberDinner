using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api;

public static class DependancyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

        services.AddMappings();
        return services;
    }
}
