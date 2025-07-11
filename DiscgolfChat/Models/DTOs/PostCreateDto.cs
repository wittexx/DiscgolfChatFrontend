namespace DiscgolfChat.Models.DTOs
{
    public class PostCreateDto
    {
        public string Content { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public int AuthorId { get; set; }
    }
}