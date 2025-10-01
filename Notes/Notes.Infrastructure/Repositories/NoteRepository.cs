using Microsoft.EntityFrameworkCore;
using Notes.Application.Abstractions;
using Notes.Domain.Entities;
using Notes.Infrastructure.Persistence;

namespace Notes.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _db;
        public NoteRepository(NotesDbContext db) => _db = db;

        public Task<Note?> GetAsync(int id, CancellationToken ct = default) => _db.Notes.FindAsync(new object[] { id }, ct).AsTask();
        public IQueryable<Note> Query() => _db.Notes.AsNoTracking();

        public async Task<Note> AddAsync(Note note, CancellationToken ct = default)
        {
            await _db.Notes.AddAsync(note, ct);
            await _db.SaveChangesAsync(ct);
            return note;
        }
        public async Task UpdateAsync(Note note, CancellationToken ct = default)
        {
            _db.Notes.Update(note);
            await _db.SaveChangesAsync(ct);
        }
        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.Notes.FindAsync(new object[] { id }, ct);
            if (entity is null) return;
            _db.Notes.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
