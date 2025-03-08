using SocialMediaAPI.models.Identity;

namespace SocialMediaAPI.models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; } = string.Empty;
        public string? MediaURL { get; set; } = string.Empty;
        public AppUser user { get; set; }
        public string userId { get; set; }
    }
}
