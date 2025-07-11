using System.ComponentModel.DataAnnotations;

namespace DiscgolfChat.Models.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kommentaren får inte vara tom.")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public UserDto? Author { get; set; }
        public string? AuthorName { get; set; }
    }
}
