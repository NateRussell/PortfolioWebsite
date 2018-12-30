using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioWebsite.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PortfolioWebsite.Constants;

namespace PortfolioWebsite.Models.SeedData
{

    public static class SeedUser
    {

        private const string TEST_PASSWORD = "123Test!";

        public static void SeedTestUsers(UserManager<AppUser> userManager)
        {
            CreateTestUser(userManager, "UserA", new Claim[] { new Claim(Roles.CLAIM_ID, Roles.ADMIN) });
            CreateTestUser(userManager, "UserB", new Claim[] { new Claim(Roles.CLAIM_ID, Roles.USER) });
            CreateTestUser(userManager, "UserC", new Claim[] { new Claim(Roles.CLAIM_ID, Roles.USER) });
        }

        private static void CreateTestUser(UserManager<AppUser> userManager, string userName, Claim[] claims)
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
                    if (claims != null)
                    {
                        userManager.AddClaimsAsync(user, claims).Wait();
                    }
                }
            }
        }

        public static void Initialize(UserManager<AppUser> userManager)
        {
            #if DEBUG
                SeedTestUsers(userManager);
            #endif
        }
    }
}
