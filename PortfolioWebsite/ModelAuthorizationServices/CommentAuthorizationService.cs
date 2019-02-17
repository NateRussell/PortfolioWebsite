using Microsoft.AspNetCore.Authorization;
using PortfolioWebsite.Constants;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioWebsite.ModelAuthorizationServices
{
    public class CommentAuthorizationService: ModelAuthorizationService, ICommentAuthorizationService
    {
        
        public CommentAuthorizationService(IAuthorizationService authorizationService): base(authorizationService)
        {
            
        }

        public bool IsAuthorizedEditor(Comment comment, ClaimsPrincipal user)
        {
            Claim userID = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                return userID.Value == comment.UserID || _authorizationService.AuthorizeAsync(user, Policies.IS_ADMIN).Result.Succeeded;
            }
            else
            {
                return false;
            }
        }
    }
}
