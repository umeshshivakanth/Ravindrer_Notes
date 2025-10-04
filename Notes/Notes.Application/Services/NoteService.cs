using AutoMapper;
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
        private readonly IMapper _mapper;
        public NoteService(INoteRepository repo, IMapper mapper)
        {
            _notesRepository = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync(CancellationToken ct = default)
        {
            var notes = await _notesRepository.Query().OrderByDescending(n => n.CreatedDate).ToListAsync(ct);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task AddNoteAsync(NoteDto noteDto, CancellationToken ct = default)
        {
            var note = _mapper.Map<Note>(noteDto);
            await _notesRepository.AddAsync(note, ct);
        }

        public async Task UpdateNoteAsync(NoteDto noteDto, CancellationToken ct = default)
        {
            var note = await _notesRepository.GetAsync(noteDto.Id, ct) ?? throw new KeyNotFoundException("Note not found");
            _mapper.Map(noteDto, note);
            note.ModifiedDate = DateTime.UtcNow;
            await _notesRepository.UpdateAsync(note, ct);
        }

        public async Task DeleteNoteAsync(int id, CancellationToken ct = default)
        {
            await _notesRepository.DeleteAsync(id, ct);
        }
    }
}
