using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioWebsite.ModelServices
{
    public interface IModelService
    {
        string GetUserID(ClaimsPrincipal user);
        string GetUserName(ClaimsPrincipal user);
        string GetUserData(ClaimsPrincipal user, string claimType);
    }
}
