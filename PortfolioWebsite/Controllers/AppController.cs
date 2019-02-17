using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Constants;
using PortfolioWebsite.Models;
using PortfolioWebsite.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioWebsite.Controllers
{
    public class AppController<T>: Controller
    {
        protected readonly ControllerResponder<T> _responder = new ControllerResponder<T>();
        protected readonly ControllerResponder<T> _AJAXresponder = new ControllerResponder<T>();
        protected readonly ControllerResponder<T> _JSONresponder = new ControllerResponder<T>();

        protected IActionResult GetResponse(IModelServiceResponse<T> modelServiceResponse, string responseType)
        {
            switch (responseType)
            {
                case ResponseTypes.AJAX:
                    return _AJAXresponder.Respond(modelServiceResponse);
                case ResponseTypes.JSON:
                    return _JSONresponder.Respond(modelServiceResponse);
                default:
                    return _responder.Respond(modelServiceResponse);
            }
        }

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
