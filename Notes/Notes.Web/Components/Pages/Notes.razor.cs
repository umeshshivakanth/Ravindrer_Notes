using global::Notes.Domain.Enums;
using global::Notes.Application.DTOs;
namespace Notes.Web.Components.Pages
{
    public partial class Notes
    {
        private IEnumerable<NoteViewModel> notes = new List<NoteViewModel>();
        private NoteViewModel note = new NoteViewModel();
        private NoteViewModel? editingNote = null;

        protected override async Task OnInitializedAsync()
        {
            await GetNotes();
        }

        private async Task SaveNote()
        {
            await notesService.AddNoteAsync(MapViewModelToDto(note), GetCancellationToken());
            await GetNotes();
            note = new NoteViewModel();
        }

        private async Task GetNotes()
        {
            var noteDtos = await notesService.GetAllNotesAsync(GetCancellationToken());
            notes = noteDtos.Select(MapDtoToViewModel);

        }

        private void EditNote(NoteViewModel note)
        {
            editingNote = new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                Priority = note.Priority
            };
        }

        private async Task SaveEdit()
        {
            if (editingNote != null)
            {
                await notesService.UpdateNoteAsync(MapViewModelToDto(editingNote), GetCancellationToken());
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

        private NoteViewModel MapDtoToViewModel(NoteDto dto) => new NoteViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            CreatedOn = dto.CreatedAt.ToLocalTime().ToString("g"),
            UpdatedOn = dto.ModifiedAt?.ToLocalTime().ToString("g"),
        };

        private NoteDto MapViewModelToDto(NoteViewModel vm) => new NoteDto
        {
            Id = vm.Id,
            Title = vm.Title,
            Description = vm.Description,
            Priority = vm.Priority,
        };
    }
}
