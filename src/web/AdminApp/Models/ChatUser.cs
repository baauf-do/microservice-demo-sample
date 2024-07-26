using MudBlazor;

namespace AdminApp.Models
{
    public class ChatUser
    {
        public string UserName { get; set; } = string.Empty;
        public string UserRoleColor { get; set; } = string.Empty;
        public Color OnlineStatus { get; set; }
        public bool Spotify { get; set; } = false;
        public string AvatarUrl { get; set; } = string.Empty;
        public Color AvatarColor { get; set; }
    }
}
