namespace DiscgolfChat.Models.DTOs
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        //tom test sak
        public List<PostDto> Posts { get; set; } = new();
    }
}