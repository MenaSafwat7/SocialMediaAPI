using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.DTOs
{
    public class RegisterDTO
    {
        public string DisplayName { get; set; }
        public string userName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{14,}$",
            ErrorMessage = "Password must contains at least 1 Upper case, 1 lower case, 1 digit, 1 special character and the maximum length is 14")]
        public string password { get; set; }
        [Phone]
        public string? phoneNumber { get; set; }
    }
}
