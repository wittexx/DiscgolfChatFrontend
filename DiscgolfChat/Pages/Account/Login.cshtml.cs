using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscgolfChat.Pages.Account
{
    public class LoginPageModel : PageModel
    {
        private readonly ApiService _api;
        private readonly IHttpContextAccessor _contextAccessor;

        [BindProperty]
        public LoginDto LoginUser { get; set; } = new();

        public LoginPageModel(ApiService api, IHttpContextAccessor contextAccessor)
        {
            _api = api;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = await _api.LoginAsync(LoginUser);
            if (userId.HasValue)
            {
                _contextAccessor.HttpContext?.Session.SetInt32("UserId", userId.Value);
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Felaktiga inloggningsuppgifter.");
            return Page();
        }
    }
}