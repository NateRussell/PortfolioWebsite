using Microsoft.AspNetCore.Authorization;
using PortfolioWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsite.ModelServices
{
    public class ModelService : IModelService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IObjectModelValidator _modelValidator;

        public ModelService(
            ApplicationDbContext context,
            IObjectModelValidator modelValidator
            )
        {
            _context = context;
            _modelValidator = modelValidator;
        }

        protected virtual ValidationStateDictionary TryValidateModel(ActionContext actionContext, object model, string prefix = "")
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();
            _modelValidator.Validate(actionContext, validationState, prefix , model);
            return validationState;
        }

        public string GetUserID(ClaimsPrincipal user)
        {
            return GetUserData(user, ClaimTypes.NameIdentifier);
        }

        public string GetUserName(ClaimsPrincipal user)
        {
            return GetUserData(user, ClaimTypes.Name);
        }

        public string GetUserData(ClaimsPrincipal user, string claimType)
        {
            Claim userData = user.FindFirst(claimType);
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
