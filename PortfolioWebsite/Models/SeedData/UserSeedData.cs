using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioWebsite.Models;
using Microsoft.AspNetCore.Identity;

namespace PortfolioWebsite.Models.SeedData
{

    public static class SeedUser
    {

        private const string TEST_PASSWORD = "123Test!";

        public static void SeedTestUsers(UserManager<AppUser> userManager)
        {
            CreateTestUser(userManager, "UserA", Roles.ADMIN);
            CreateTestUser(userManager, "UserB", Roles.USER);
            CreateTestUser(userManager, "UserC", Roles.USER);
        }

        private static void CreateTestUser(UserManager<AppUser> userManager, string userName, string role)
        {
            if (userManager.FindByNameAsync(userName).Result == null)
            {
                string email = string.Format("{0}@test.com", userName);
                AppUser user = new AppUser
                {
                    UserName = email,
                    Email = email
                };
                IdentityResult result = userManager.CreateAsync(user, TEST_PASSWORD).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            CreateRole(roleManager, Roles.USER);
            CreateRole(roleManager, Roles.ADMIN);
        }

        private static void CreateRole(RoleManager<IdentityRole> roleManager, string role)
        {
            if ( !roleManager.RoleExistsAsync(role).Result)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = role
                };
                roleManager.CreateAsync(identityRole).Wait();
            }
        }

        public static void Initialize(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            SeedRoles(roleManager); //Roles must be seeded first to be attached to a user.

            #if DEBUG
                SeedTestUsers(userManager);
            #endif
        }
    }
}
