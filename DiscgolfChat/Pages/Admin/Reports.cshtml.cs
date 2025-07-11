using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DiscgolfChat.Models.DTOs;
using DiscgolfChat.Services;

namespace DiscgolfChat.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly ApiService _api;
        public ReportsModel(ApiService api) => _api = api;

        public List<ReportDto> Reports { get; set; } = new();

        public async Task OnGetAsync()
        {
            Reports = await _api.GetAllReportsAsync();
        }

        public async Task<IActionResult> OnPostReviewAsync(int id, bool offensive)
        {
            await _api.ReviewReportAsync(id, offensive);
            return RedirectToPage();
        }
    }
}