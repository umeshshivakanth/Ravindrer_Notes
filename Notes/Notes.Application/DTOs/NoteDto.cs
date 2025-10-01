using Notes.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Notes.Application.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title must be under 100 characters")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Priority is required")]
        public Priority Priority { get; set; } = Priority.Low;
        public string CreatedBy { get; set; } = "Admin";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
