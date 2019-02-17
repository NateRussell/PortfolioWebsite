using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.ModelAuthorizationServices;

namespace PortfolioWebsite.ModelServices
{
    public interface ICommentService : IModelService
    {
        ICommentAuthorizationService Authorization { get; }
        Task<IModelServiceResponse<Comment>> Create(ActionContext actionContext, Comment comment);
        Task<IModelServiceResponse<Comment>> Reply(ActionContext actionContext, Comment comment);
        Task<IModelServiceResponse<Comment>> Edit(ActionContext actionContext, int id, string text);
        Task<IModelServiceResponse<Comment>> Delete(ActionContext actionContext, int id);

    }
}
