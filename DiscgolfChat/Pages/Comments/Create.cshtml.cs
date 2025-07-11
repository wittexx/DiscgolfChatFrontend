using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DiscgolfChat.Services;
using DiscgolfChat.Models.DTOs;

namespace DiscgolfChat.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _api;
        public CreateModel(ApiService api) => _api = api;

        [BindProperty(SupportsGet = true)]
        public int PostId { get; set; }

        [BindProperty]
        public CommentCreateDto Comment { get; set; } = new();  

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Account/Login");

            Comment.PostId = PostId;
            Comment.AuthorId = userId.Value;

            Console.WriteLine($"🛠 Skickar kommentar: PostId={Comment.PostId}, AuthorId={Comment.AuthorId}, Content={Comment.Content}");

            var success = await _api.CreateCommentAsync(Comment);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Kunde inte skapa kommentar.");
                return Page();
            }

            var post = await _api.GetPostByIdAsync(PostId);
            if (post == null)
                return RedirectToPage("/Index");

            return RedirectToPage("/Index");
        }
    }
}