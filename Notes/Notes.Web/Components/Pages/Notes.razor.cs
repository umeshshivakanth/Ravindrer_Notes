using global::Notes.Domain.Enums;
using global::Notes.Application.DTOs;
using Notes.Web.ViewModels;
using Notes.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Components;
namespace Notes.Web.Components.Pages
{
    public partial class Notes
    {
        private IEnumerable<NoteViewModel> notes = new List<NoteViewModel>();
        private NoteViewModel note = new NoteViewModel();
        private NoteViewModel? editingNote = null;
        [Inject]
        private IMapper _mapper { get; set; } = default!;
        CancellationTokenSource cts;

        protected override async Task OnInitializedAsync()
        {
            cts = new CancellationTokenSource();
            await GetNotesAsync();
        }

        private async Task SaveNoteAsync()
        {
            await notesService.AddNoteAsync(_mapper.Map<NoteDto>(note), cts.Token);
            await GetNotesAsync();
            note = new NoteViewModel();
        }

        private async Task GetNotesAsync()
        {
            var noteDtos = await notesService.GetAllNotesAsync(cts.Token);
            notes = noteDtos.Select(_mapper.Map<NoteViewModel>);
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

        private async Task SaveEditAsync()
        {
            if (editingNote != null)
            {
                await notesService.UpdateNoteAsync(_mapper.Map<NoteDto>(editingNote), cts.Token);
                await GetNotesAsync();
                editingNote = null;
            }
        }

        private void CancelEdit()
        {
            editingNote = null;
        }

        private async Task DeleteNote(int id)
        {
            await notesService.DeleteNoteAsync(id, cts.Token);
            await GetNotesAsync();
        }

        private string GetBadgeCss(Priority priority) => priority switch
        {
            Priority.High => "note-priority priority-high",
            Priority.Medium => "note-priority priority-medium",
            Priority.Low => "note-priority priority-low",
            _ => "note-priority priority-low",
        };

        private string GetCardBorderCss(Priority p) => p switch
        {
            Priority.High => "priority-border-high",
            Priority.Medium => "priority-border-medium",
            Priority.Low => "priority-border-low",
            _ => "priority-border-low"
        };
    }
}
