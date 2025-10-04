using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application;
using Notes.Infrastructure;
namespace Notes.Composition;

public static class DependencyInjection
{
    public static IServiceCollection AddNotesPlatform(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        return services;
    }
}