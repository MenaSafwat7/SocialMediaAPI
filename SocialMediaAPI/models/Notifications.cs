using SocialMediaAPI.models.Identity;

namespace SocialMediaAPI.models
{
    public class Notifications
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}
