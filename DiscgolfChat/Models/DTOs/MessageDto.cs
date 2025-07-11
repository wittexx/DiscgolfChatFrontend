namespace DiscgolfChat.Models.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; } = "";
        public DateTime SentAt { get; set; }
        public int AuthorId { get; set; }

        // För visning
        public string FromUsername { get; set; } = "";
        public string ToUsername { get; set; } = "";
    }
}