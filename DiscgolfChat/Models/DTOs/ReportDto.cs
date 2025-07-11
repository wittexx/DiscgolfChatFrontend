namespace DiscgolfChat.Models.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string? PostContent { get; set; }
        public string? CommentContent { get; set; }
        public bool Reviewed { get; set; }
        public bool IsOffensive { get; set; }
    }

}
