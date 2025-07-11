using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscgolfChat.Pages.Topics
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _api;
        public DetailsModel(ApiService api) => _api = api;

        public TopicDto? Topic { get; set; }
        public List<PostDto> Posts { get; set; } = new();

        [BindProperty]
        public PostCreateDto NewPost { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Topic = await _api.GetTopicByIdAsync(id);
            if (Topic == null) return NotFound();

            Posts = await _api.GetPostsForTopicAsync(id);

            foreach (var post in Posts)
            {
                post.Comments = await _api.GetCommentsForPostAsync(post.Id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostReportAsync(int? postId, int? commentId, int id)
        {
            if (postId == null && commentId == null)
                return RedirectToPage(new { id });

            var report = new ReportDto
            {
                PostId = postId,
                CommentId = commentId
            };

            await _api.ReportPostAsync(report);

            
            Topic = await _api.GetTopicByIdAsync(id);
            Posts = await _api.GetPostsForTopicAsync(id);
            foreach (var post in Posts)
            {
                post.Comments = await _api.GetCommentsForPostAsync(post.Id);
            }

            return RedirectToPage(new { id }); 
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Account/Login");

            NewPost.TopicId = id;
            NewPost.AuthorId = userId.Value;

            var success = await _api.CreatePostAsync(NewPost);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Kunde inte skapa inlägg.");
                return await OnGetAsync(id); 
            }

            return RedirectToPage(new { id });
        }
    }
}