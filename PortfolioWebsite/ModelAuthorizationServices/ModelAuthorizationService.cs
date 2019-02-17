using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.ModelAuthorizationServices
{
    public class ModelAuthorizationService
    {
        protected readonly IAuthorizationService _authorizationService;
        public ModelAuthorizationService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
    }
}
