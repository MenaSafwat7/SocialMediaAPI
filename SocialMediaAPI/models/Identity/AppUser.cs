using Microsoft.AspNetCore.Identity;

namespace SocialMediaAPI.models.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string? ProfilePictureURL { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
    }
}
