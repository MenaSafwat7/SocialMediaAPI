using SocialMediaAPI.models.Identity;

namespace SocialMediaAPI.models
{
    public class Likes
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}
