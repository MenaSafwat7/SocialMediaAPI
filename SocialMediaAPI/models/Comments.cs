using SocialMediaAPI.models.Identity;

namespace SocialMediaAPI.models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}
