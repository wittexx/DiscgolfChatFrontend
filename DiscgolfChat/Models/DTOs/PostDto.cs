namespace DiscgolfChat.Models.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TopicId { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public TopicDto? Topic { get; set; }

        public List<CommentDto> Comments { get; set; } = new();
    }
}
