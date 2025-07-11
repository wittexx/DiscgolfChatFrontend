using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DiscgolfChat.Services;
using DiscgolfChat.Models.DTOs;

namespace DiscgolfChat.Pages.Admin
{
    public class CreateTopicModel : PageModel
    {
        private readonly ApiService _api;

        public CreateTopicModel(ApiService api)
        {
            _api = api;
        }

        [BindProperty]
        public TopicCreateDto NewTopic { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _api.CreateTopicAsync(NewTopic);
            return RedirectToPage("/Index");
        }
    }
}