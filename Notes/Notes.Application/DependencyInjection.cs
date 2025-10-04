using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Mapping;
using Notes.Application.Services;

namespace Notes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<NoteMappingProfile>());
        services.AddScoped<INotesService, NoteService>();

        return services;
    }
}