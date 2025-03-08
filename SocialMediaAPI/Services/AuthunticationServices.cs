using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaAPI.DTOs;
using SocialMediaAPI.Exceptions;
using SocialMediaAPI.models.Identity;
using SocialMediaAPI.Services.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaAPI.Services
{
    public class AuthunticationServices(UserManager<AppUser> userManager, IOptions<JwtOptions> options, IMapper mapper) : IAuthunticationServices
    {
        public async Task<bool> CheckEmailExist(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            return user != null;
        }

        public async Task<UserDTO> GetUserByEmail(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email) ?? throw new Exception($"{Email} not found");

            return new UserDTO(
                user.DisplayName,user.Email,await CreateTokenAsync(user));
        }

        public async Task<UserDTO> LoginAsync(LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user == null) throw new Exception($"Email {login.Email} doesn't Exist.");

            var result = await userManager.CheckPasswordAsync(user, login.Password);

            if (!result) throw new Exception("Invalid Email or password");

            return new UserDTO(
              user.DisplayName,
              user.Email!,
              await CreateTokenAsync(user));
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO register)
        {
            var user = new AppUser
            {
                Email = register.email,
                DisplayName = register.DisplayName,
                UserName = register.DisplayName,
                PhoneNumber = register.phoneNumber
            };

            var result = await userManager.CreateAsync(user, register.password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                throw new Exceptions.ValidationException(errors);
            }

            return new UserDTO(
             user.DisplayName,
             user.Email!,
             await CreateTokenAsync(user));
        }

        private async Task<string> CreateTokenAsync(AppUser user)
        {
            var jwtOptions = options.Value;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: authClaims,
                signingCredentials: creds,
                audience: jwtOptions.Audience,
                issuer: jwtOptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
