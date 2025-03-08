using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SocialMediaAPI.DTOs;
using SocialMediaAPI.models.Identity;
using SocialMediaAPI.Services.Abstraction;
using System.Security.Claims;

namespace SocialMediaAPI.Controllers
{
    public class AccountController(IAuthunticationServices Authentication) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
            => Ok(await Authentication.LoginAsync(login));

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
            => Ok(await Authentication.RegisterAsync(register));


        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return Ok(await Authentication.CheckEmailExist(email));
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await Authentication.GetUserByEmail(email);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<AppUser> users = [
                new AppUser(){ UserName = "Mena Safwat" , Bio = "dddddddddd", DisplayName = "dfdfdfd" , Email = "sddddddd"},
                new AppUser(){ UserName = "Mena amir" , Bio = "dddddddddd", DisplayName = "dfdfdfd" , Email = "sddddddd"}
                ];
            return Ok(users);
        }


    }
}
