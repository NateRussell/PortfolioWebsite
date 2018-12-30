using Microsoft.AspNetCore.Authorization;
using PortfolioWebsite.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite
{
    public class PolicyConfiguration
    {
        public PolicyConfiguration(AuthorizationOptions options)
        {
            options.AddPolicy(Policies.IS_ADMIN, policy =>
            {
                policy.RequireClaim(Roles.CLAIM_ID, Roles.ADMIN);
            });
        }
    }
}
