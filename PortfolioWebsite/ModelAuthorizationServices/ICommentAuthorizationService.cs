using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioWebsite.ModelAuthorizationServices
{
    public interface ICommentAuthorizationService
    {
        bool IsAuthorizedEditor(Comment comment, ClaimsPrincipal user);
    }
}
