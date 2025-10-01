using Notes.Domain.Entities;
namespace Notes.Application.Abstractions
{
    public interface INoteRepository
    {
        Task<Note?> GetAsync(int id, CancellationToken ct = default);
        IQueryable<Note> Query();
        Task<Note> AddAsync(Note note, CancellationToken ct = default);
        Task UpdateAsync(Note note, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
