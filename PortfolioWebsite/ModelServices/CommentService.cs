using Microsoft.AspNetCore.Authorization;
using PortfolioWebsite.Data;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsite.ModelServices
{
    public class CommentService : ModelService , ICommentService
    {
        public CommentService(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IObjectModelValidator modelValidator
            )
            :base(context, authorizationService, modelValidator)
        {

        }

        public async Task<IModelServiceResponse<Comment>> Create(ActionContext actionContext, Comment comment)
        {
            comment = comment.Clone();

            string userID = GetUserID(actionContext.HttpContext.User);
            if (userID != "")
            {
                comment.UserID = userID;
            }

            ValidationStateDictionary validationState = TryValidateModel(actionContext, comment);

            if (!validationState.IsValid()) //If Comment does not have validation errors
            {
                return new ModelServiceResponse<Comment>(comment, validationState, HttpStatusCode.BadRequest);
            }

            try
            {
                //Save the comment to the database
                _context.Add(comment);
                await _context.SaveChangesAsync();

                //
                return new ModelServiceResponse<Comment>(comment, validationState, HttpStatusCode.Created);
            }
            catch
            {
                //
                return new ModelServiceResponse<Comment>(null, validationState, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IModelServiceResponse<Comment>> Reply(ActionContext actionContext, Comment comment)
        {
            comment = comment.Clone();

            //If new comment has a parent Comment ID, set the comment to have the same Work ID as the parent Comment.
            if (comment.ParentID != null)
            {
                Comment parentComment = await _context.Comment.FindAsync(comment.ParentID);
                if (parentComment != null)
                {
                    comment.WorkID = parentComment.WorkID;
                }
            }

            return await Create(actionContext, comment);
        }

        public async Task<IModelServiceResponse<Comment>> Edit(ActionContext actionContext, int commentId, string text)
        {
            Comment comment = await Find(commentId);
            if (comment == null)
            {
                return new ModelServiceResponse<Comment>(null, null, HttpStatusCode.NotFound);
            }
            else
            {
                comment.Text = text;
                comment.Edited = true;
                comment.EditDate = DateTime.UtcNow;
                return await Update(actionContext, comment);
            }
        }

        

        public async Task<IModelServiceResponse<Comment>> Delete(ActionContext actionContext, int commentId)
        {
            Comment comment = await Find(commentId);
            if (comment == null)
            {
                return new ModelServiceResponse<Comment>(null, null, HttpStatusCode.NotFound);
            }
            else
            {
                comment.Deleted = true;
                comment.DeleteDate = DateTime.UtcNow;
                return await Update(actionContext, comment);
            }
        }

        private async Task<IModelServiceResponse<Comment>> Update(ActionContext actionContext, Comment comment)
        {
            if (!comment.IsAuthorizedEditor(actionContext.HttpContext.User, _authorizationService))
            {
                return new ModelServiceResponse<Comment>(comment, null, HttpStatusCode.Unauthorized);
            }

            ValidationStateDictionary validationState = TryValidateModel(actionContext, comment);
            if (!validationState.IsValid())
            {
                return new ModelServiceResponse<Comment>(comment, validationState, HttpStatusCode.BadRequest);
            }

            try
            {
                _context.Update(comment);
                await _context.SaveChangesAsync();

                return new ModelServiceResponse<Comment>(comment, validationState, HttpStatusCode.OK);
            }
            catch
            {
                return new ModelServiceResponse<Comment>(null, validationState, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Comment> Find(int commentId)
        {
            return await _context.Comment.FindAsync(commentId);
        }
    }
}
