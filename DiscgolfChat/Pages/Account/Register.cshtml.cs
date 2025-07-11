using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscgolfChat.Pages.Account
{
    public class RegisterPageModel : PageModel
    {
        private readonly ApiService _api;

        [BindProperty]
        public RegisterDto NewUser { get; set; } = new();

        public RegisterPageModel(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = await _api.RegisterAsync(NewUser);
            if (!success)
                ModelState.AddModelError(string.Empty, "Registrering misslyckades.");

            return RedirectToPage("/Account/Login");
        }
    }
}