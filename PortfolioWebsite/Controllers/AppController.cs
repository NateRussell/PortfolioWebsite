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
            Claim userID = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                return userID.Value;
            }
            else
            {
                return "";
            }
        }
    }
}
