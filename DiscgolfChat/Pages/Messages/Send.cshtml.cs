using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;

namespace DiscgolfChat.Pages.Messages
{
    public class SendModel : PageModel
    {
        private readonly ApiService _api;

        public SendModel(ApiService api)
        {
            _api = api;
        }

        [BindProperty]
        public MessageDto Message { get; set; } = new();

        public string ReceiverName { get; set; } = "";

        public async Task<IActionResult> OnGetAsync(int? toUserId)
        {
            var fromUserId = HttpContext.Session.GetInt32("UserId");
            Console.WriteLine($"➡️ OnGetAsync: fromUserId={fromUserId}, toUserId={toUserId}");

            if (fromUserId == null || toUserId == null)
            {
                Console.WriteLine("❌ Inloggning saknas eller mottagare-ID är null");
                return RedirectToPage("/Account/Login");
            }

            Message.ToUserId = toUserId.Value;

            Console.WriteLine($"➡️ Försöker hämta mottagare med ID: {toUserId.Value}");
            var receiver = await _api.GetUserByIdAsync(toUserId.Value);

            if (receiver == null)
            {
                Console.WriteLine("❌ Mottagare hittades inte i API:t");
                TempData["Error"] = "Användaren du försöker kontakta finns inte.";
                return RedirectToPage("/Index");
            }

            ReceiverName = receiver.Username;
            Console.WriteLine($"✅ Mottagare hittad: {ReceiverName}");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var fromUserId = HttpContext.Session.GetInt32("UserId");
            Console.WriteLine($"➡️ OnPostAsync: fromUserId={fromUserId}, toUserId={Message.ToUserId}");

            if (fromUserId == null)
            {
                Console.WriteLine("❌ Användaren är inte inloggad");
                return RedirectToPage("/Account/Login");
            }

            Message.FromUserId = fromUserId.Value;
            Message.SentAt = DateTime.Now;

            Console.WriteLine($"✉️ Skickar meddelande till {Message.ToUserId} från {Message.FromUserId}");

            await _api.SendMessageAsync(Message);

            Console.WriteLine("✅ Meddelande skickat!");
            return RedirectToPage("/Messages/Inbox");
        }
    }
}