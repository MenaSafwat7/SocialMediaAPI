﻿using Microsoft.AspNetCore.Identity;

namespace SocialMediaAPI.models.Identity
{
    public static class SocialMediaDbContetSeed
    {
        public static async Task SeedUserAsync(UserManager<IdentityUser> manager)
        {
            if (!manager.Users.Any())
            {
                var user = new IdentityUser()
                {
                    Email = "menajesus2003@gmail.com",
                    UserName = "MenaSafwat2003",
                    PhoneNumber = "01274707196"
                };
                await manager.CreateAsync(user, "Pa$$word123");
            }
        }
    }
}
