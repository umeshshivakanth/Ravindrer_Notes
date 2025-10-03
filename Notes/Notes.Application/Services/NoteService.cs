using Microsoft.EntityFrameworkCore;
using Notes.Application.Abstractions;
using Notes.Application.DTOs;
using Notes.Domain.Entities;

namespace Notes.Application.Services
{
    public interface INotesService
    {
        Task<IEnumerable<NoteDto>> GetAllNotesAsync(CancellationToken ct);
        Task AddNoteAsync(NoteDto note, CancellationToken ct);
        Task UpdateNoteAsync(NoteDto noteDto, CancellationToken ct);
        Task DeleteNoteAsync(int id, CancellationToken ct);
    }

    public class NoteService : INotesService
    {
        private readonly INoteRepository _notesRepository;
        public NoteService(INoteRepository repo) => _notesRepository = repo;

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync(CancellationToken ct = default)
        {
            var notes = await _notesRepository.Query().OrderByDescending(n => n.CreatedDate).ToListAsync(ct);
            return notes.Select(n => new NoteDto { Id = n.Id, Title = n.Title, Description = n.Description, Priority = n.Priority, CreatedBy = n.CreatedBy, CreatedAt = n.CreatedDate, ModifiedAt = n.ModifiedDate });
        }

        public async Task AddNoteAsync(NoteDto noteDto, CancellationToken ct = default)
        {
            var note = new Note { Id = noteDto.Id, Title = noteDto.Title, Description = noteDto.Description, Priority = noteDto.Priority, CreatedBy = noteDto.CreatedBy, CreatedDate = DateTime.UtcNow };
            await _notesRepository.AddAsync(note, ct);
        }

        public async Task UpdateNoteAsync(NoteDto noteDto, CancellationToken ct = default)
        {
            var note = await _notesRepository.GetAsync(noteDto.Id, ct) ?? throw new KeyNotFoundException("Note not found");
            note.Title = noteDto.Title.Trim();
            note.Description = noteDto.Description.Trim();
            note.Priority = noteDto.Priority;
            note.ModifiedDate = DateTime.UtcNow;
            await _notesRepository.UpdateAsync(note, ct);
        }

        public async Task DeleteNoteAsync(int id, CancellationToken ct = default)
        {
            await _notesRepository.DeleteAsync(id, ct);
        }
    }
}
