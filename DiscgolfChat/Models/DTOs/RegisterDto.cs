﻿namespace DiscgolfChat.Models.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
