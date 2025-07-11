using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscgolfChat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _api;

        public List<TopicDto> Topics { get; set; } = new();

        public IndexModel(ApiService api)
        {
            _api = api;
        }

        public async Task OnGetAsync()
        {
            Topics = await _api.GetTopicsAsync();
        }
    }
}