using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DiscgolfChat.Services;
using DiscgolfChat.Models.DTOs;

namespace DiscgolfChat.Pages.Messages
{
    public class InboxModel : PageModel
    {
        private readonly ApiService _api;

        public InboxModel(ApiService api)
        {
            _api = api;
        }

        public List<MessageDto> Messages { get; set; } = new();
        public Dictionary<int, string> UserNames { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Account/Login");

            Messages = await _api.GetInboxAsync(userId.Value);

            // 🔍 Hämta alla användare
            var users = await _api.GetAllUsersAsync();
            UserNames = users.ToDictionary(u => u.Id, u => u.Username);

            return Page();
        }
    }
}
