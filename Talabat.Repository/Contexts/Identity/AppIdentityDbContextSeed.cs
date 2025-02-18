using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Contexts.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Abdelrahman Mohamed",
                    Email = "abdelrahmanmohamed2293@gmail.com",
                    UserName = "abdelrahman22",
                    PhoneNumber = "01015496488",
                };

                await userManager.CreateAsync(User, "Pa$$w0rd");
            }
        }
    }
}
