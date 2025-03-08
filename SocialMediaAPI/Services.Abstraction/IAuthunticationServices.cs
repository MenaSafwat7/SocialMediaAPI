using SocialMediaAPI.DTOs;

namespace SocialMediaAPI.Services.Abstraction
{
    public interface IAuthunticationServices
    {
        public Task<UserDTO> LoginAsync (LoginDTO login);
        public Task<UserDTO> RegisterAsync (RegisterDTO register);
        public Task<UserDTO> GetUserByEmail (string Email);
        public Task<bool> CheckEmailExist (string Email);

    }
}
