using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscgolfChat.Pages.Topics
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _api;
        public CreateModel(ApiService api) => _api = api;

        [BindProperty]
        public TopicCreateDto Topic { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = await _api.CreateTopicAsync(Topic);
            if (!success)
                ModelState.AddModelError(string.Empty, "Kunde inte skapa ämnet.");

            return RedirectToPage("/Index");
        }
    }
}