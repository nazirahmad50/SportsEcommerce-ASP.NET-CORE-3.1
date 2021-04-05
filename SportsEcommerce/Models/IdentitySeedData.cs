using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public static class IdentitySeedData
    {
        private const string adminuser = "Admin";
        private const string adminPassword = "Secret123@";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationIdentityDbContext context = app.ApplicationServices.CreateScope()
                                                        .ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope().ServiceProvider
                                                        .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminuser);

            if (user == null)
            {
                user = new IdentityUser("Admin");
                user.Email = "admin@example.com";
                user.PhoneNumber = "444-1234";
                await userManager.CreateAsync(user, adminPassword);
            }
        }

    }
}
