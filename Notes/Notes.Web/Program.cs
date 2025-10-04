using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Abstractions;
using Notes.Application.Services;
using Notes.Infrastructure.Persistence;
using Notes.Infrastructure.Repositories;
using Notes.Web.Components;
using Notes.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAutoMapper(cfg => { }, typeof(NoteMappingProfile));

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("Notes.Infrastructure")
    )
);

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INotesService, NoteService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
