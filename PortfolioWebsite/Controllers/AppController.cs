using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioWebsite.Controllers
{
    public class AppController: Controller
    {
        protected string GetUserID()
        {
            return GetUserData(ClaimTypes.NameIdentifier);
        }

        protected string GetUserName()
        {
            return GetUserData(ClaimTypes.Name);
        }

        protected string GetUserData(string claimType)
        {
            Claim userData = User.FindFirst(claimType);
            if (userData != null)
            {
                return userData.Value;
            }
            else
            {
                return "";
            }
        }
    }
}
