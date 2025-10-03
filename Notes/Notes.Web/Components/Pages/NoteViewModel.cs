using Notes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Notes.Web.Components.Pages
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(140, ErrorMessage = "Title must be under 140 characters")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Priority is required")]
        public Priority Priority { get; set; } = Priority.Low;
        public string CreatedOn { get; set; } = "";
        public string? UpdatedOn { get; set; } = "";
    }
}
