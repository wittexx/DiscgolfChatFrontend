using DiscgolfChat.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace DiscgolfChat.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _context;

        public ApiService(HttpClient http, IHttpContextAccessor context)
        {
            _http = http;
            //_http.BaseAddress = new Uri("https://discgolfchat-api-aubpa6fwh8fecxdg.swedencentral-01.azurewebsites.net/"); // azure funka inte 
            _context = context;
        }

        //  Hämta alla ämnen
        public async Task<List<TopicDto>> GetTopicsAsync()
        {
            return await _http.GetFromJsonAsync<List<TopicDto>>("api/topics") ?? new();
        }

        //  Hämta ett specifikt ämne
        public async Task<TopicDto?> GetTopicByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<TopicDto>($"api/topics/{id}");
        }

        //  Skapa nytt ämne
        public async Task<bool> CreateTopicAsync(TopicCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/topics", dto);
            return response.IsSuccessStatusCode;
        }

        //  Hämta inlägg för ämne
        public async Task<List<PostDto>> GetPostsForTopicAsync(int topicId)
        {
            return await _http.GetFromJsonAsync<List<PostDto>>($"api/posts/topic/{topicId}") ?? new();
        }

        //  Skapa nytt inlägg
        public async Task<bool> CreatePostAsync(PostCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/posts", dto);
            return response.IsSuccessStatusCode;
        }

        //  Hämta specifikt inlägg
        public async Task<PostDto?> GetPostByIdAsync(int postId)
        {
            return await _http.GetFromJsonAsync<PostDto>($"api/posts/{postId}");
        }



        //  Hämta kommentarer för inlägg
        public async Task<bool> CreateCommentAsync(CommentCreateDto comment)
        {
            var response = await _http.PostAsJsonAsync("api/comments", comment);
            return response.IsSuccessStatusCode;
        }
        //  Registrera
        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/account/register", dto);
            return response.IsSuccessStatusCode;
        }

        //  Logga in
        public async Task<int?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/account/login", dto);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonDocument.Parse(json).RootElement;

            int userId = result.GetProperty("id").GetInt32(); // gemener!
            bool isAdmin = result.GetProperty("isAdmin").GetBoolean();

            _context.HttpContext?.Session.SetInt32("UserId", userId);
            _context.HttpContext?.Session.SetString("IsAdmin", isAdmin.ToString().ToLower());

            return userId;
        }

        //  Meddelanden
        public async Task<bool> SendMessageAsync(MessageDto message)
        {
            var response = await _http.PostAsJsonAsync("api/messages", message);
            return response.IsSuccessStatusCode;
        }
        // Meddelanden
        public async Task<List<MessageDto>> GetInboxAsync(int userId)
        {
            return await _http.GetFromJsonAsync<List<MessageDto>>($"api/messages/inbox/{userId}") ?? new();
        }


        // raportering 
        public async Task<bool> MarkAsReviewedAsync(int postId)
        {
            var response = await _http.PutAsync($"api/reports/{postId}/review", null);
            return response.IsSuccessStatusCode;
        }
       
      
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDto>>("api/account/users") ?? new();
        }
       
      
        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _http.GetFromJsonAsync<UserDto>($"api/account/users/{userId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Kunde inte hämta användare {userId}: {ex.Message}");
                return null;
            }
        }



        public async Task<List<CommentDto>> GetCommentsForPostAsync(int postId)
        {
            var response = await _http.GetAsync($"api/comments/post/{postId}");
            if (response.IsSuccessStatusCode)
            {
                var comments = await response.Content.ReadFromJsonAsync<List<CommentDto>>();
                return comments ?? new List<CommentDto>();
            }

            return new List<CommentDto>();
        }

        // raportering 
        public async Task<bool> ReportPostAsync(ReportDto report)
        {
            var response = await _http.PostAsJsonAsync("api/reports", report);
            return response.IsSuccessStatusCode;
        }



        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            return await _http.GetFromJsonAsync<List<PostDto>>("api/posts") ?? new();
        }


        // raportering 
        public async Task<List<ReportDto>> GetAllReportsAsync()
        {
            return await _http.GetFromJsonAsync<List<ReportDto>>("api/reports") ?? new();
        }

        // raportering 
        public async Task<bool> ReviewReportAsync(int reportId, bool isOffensive)
        {
            var response = await _http.PutAsJsonAsync($"api/reports/{reportId}/review", isOffensive);
            return response.IsSuccessStatusCode;
        }
    }
}