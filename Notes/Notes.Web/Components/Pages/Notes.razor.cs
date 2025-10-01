using global::Notes.Domain.Enums;
using global::Notes.Application.DTOs;
namespace Notes.Web.Components.Pages
{
    public partial class Notes
    {
        private IEnumerable<NoteDto> notes;
        private NoteDto note = new NoteDto();
        private NoteDto? editingNote = null;

        protected override async Task OnInitializedAsync()
        {
            await GetNotes();
        }

        private async Task SaveNote()
        {
            await notesService.AddNoteAsync(note, GetCancellationToken());
            await GetNotes();
            note = new NoteDto();
        }

        private async Task GetNotes()
        {
            notes = await notesService.GetAllNotesAsync(GetCancellationToken());
        }

        private void EditNote(NoteDto note)
        {
            editingNote = new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                Priority = note.Priority,
                CreatedAt = note.CreatedAt
            };
        }

        private async Task SaveEdit()
        {
            if (editingNote != null)
            {
                await notesService.UpdateNoteAsync(editingNote, GetCancellationToken());
                await GetNotes();
                editingNote = null;
            }
        }

        private void CancelEdit()
        {
            editingNote = null;
        }

        private async Task DeleteNote(int id)
        {
            await notesService.DeleteNoteAsync(id, GetCancellationToken());
            await GetNotes();
        }

        private CancellationToken GetCancellationToken()
        {
            var cts = new CancellationTokenSource();
            return cts.Token;
        }

        private string GetBadge(Priority priority) => priority switch
        {
            Priority.High => "badge bg-danger",
            Priority.Medium => "badge bg-warning text-dark",
            Priority.Low => "badge bg-success",
            _ => "badge bg-secondary"
        };

        private string GetCardBorder(Priority p) => p switch
        {
            Priority.High => "border-danger",
            Priority.Medium => "border-warning",
            Priority.Low => "border-success",
            _ => "border-secondary"
        };
    }
}
