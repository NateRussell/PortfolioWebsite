using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;
using PortfolioWebsite.Models;
using PortfolioWebsite.Constants;
using PortfolioWebsite.ModelServices;
using System.Net;

namespace PortfolioWebsite.Controllers
{
    public class CommentsController : AppController<Comment>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICommentService _commentService;

        public CommentsController(ApplicationDbContext context, IAuthorizationService authorizationService, ICommentService commentService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _commentService = commentService;
            SetResponses();
        }

        private void SetResponses()
        {
            //View Responses
            _responder.Default = (modelServiceResponse) => RedirectToAction(nameof(WorksController.Details), "Works", new { id = modelServiceResponse.Model.WorkID });

            //JSON Responses
            _JSONresponder.Default = (modelServiceResponse) => Json(modelServiceResponse.Model);
            _JSONresponder[HttpStatusCode.Unauthorized] = (modelServiceResponse) => Unauthorized();
            _JSONresponder[HttpStatusCode.BadRequest] = (modelServiceResponse) => BadRequest(modelServiceResponse.ValidationState);
            _JSONresponder[HttpStatusCode.NotFound] = (modelServiceResponse) => NotFound();
            
            //AJAX Responses
            _AJAXresponder[HttpStatusCode.OK, HttpStatusCode.Created] = modelServiceResponse => {
                _context.Entry(modelServiceResponse.Model).Reference(c => c.User).Load();
                return PartialView("_Index", new List<Comment> { modelServiceResponse.Model });
            };
            _AJAXresponder[HttpStatusCode.Unauthorized] = _JSONresponder[HttpStatusCode.Unauthorized];
            _AJAXresponder[HttpStatusCode.NotFound] = _JSONresponder[HttpStatusCode.NotFound];
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            string userID = GetUserID();
            var applicationDbContext = _context.Comment.Where(c => c.UserID == userID).Include(c => c.User).Include(c => c.Work).Include(c => c.Replies);
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Text, WorkID")] Comment comment, string response = "")
        {
            IModelServiceResponse<Comment> modelServiceResponse = await _commentService.Create(ControllerContext, comment);
            return GetResponse(modelServiceResponse, response);
        }

        // GET: Comments/Reply
        public async Task<IActionResult> Reply(int id, string response = "")
        {
            Comment replyComment = new Comment() { ParentID = id };
            switch (response)
            {
                case ResponseTypes.AJAX:
                    return PartialView("_Reply", replyComment);
                default:
                    replyComment.Parent = await _context.Comment.FindAsync(replyComment.ParentID);
                    return View(replyComment);
            }
        }

        // POST: Comments/Reply
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply([Bind("Text, ParentID")] Comment comment, string response = "")
        {
            IModelServiceResponse<Comment> modelServiceResponse = await _commentService.Reply(ControllerContext, comment);
            return GetResponse(modelServiceResponse, response);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            if (comment.IsAuthorizedEditor(User, _authorizationService))
            {
                return View(comment);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Text, string response = "")
        {
            IModelServiceResponse<Comment> modelServiceResponse = await _commentService.Edit(ControllerContext, id, Text);
            return GetResponse(modelServiceResponse, response);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            if (comment.IsAuthorizedEditor(User, _authorizationService))
            {
                return View(comment);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string response = "")
        {
            IModelServiceResponse<Comment> modelServiceResponse = await _commentService.Delete(ControllerContext, id);
            return GetResponse(modelServiceResponse, response);

        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.ID == id);
        }
    }
}
