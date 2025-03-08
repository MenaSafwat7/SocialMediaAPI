using SocialMediaAPI.models.Identity;

namespace SocialMediaAPI.models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public AppUser Sender { get; set; }
        public string SenderId { get; set; }
        public AppUser Receiver { get; set; }
        public string ReceiverId { get; set; }
    }
}
