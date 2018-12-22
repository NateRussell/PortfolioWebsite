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

namespace PortfolioWebsite.Controllers
{
    public class CommentsController : AppController
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userID = GetUserID();
            var applicationDbContext = _context.Comment.Where(c => c.UserID == userID).Include(c => c.User).Include(c => c.Work).Include(c => c.Replies);
            ViewData["UserID"] = userID;
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Text,WorkID,ParentID")] Comment comment)
        {
            string userID = GetUserID();
            if (userID != "")
            {
                ModelState.Clear();
                comment.UserID = userID;
                TryValidateModel(comment);
            }

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(WorksController.Details), "Works", new { id = comment.WorkID });
            }
            return BadRequest(ModelState);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
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

            if (IsValidEditor(comment))
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
        [Authorize]
        public async Task<IActionResult> Edit(int id, string Text)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            if (IsValidEditor(comment))
            {
                comment.Text = Text;
                comment.Edited = true;
                comment.EditDate = DateTime.UtcNow;
                TryValidateModel(comment);
            }
            else
            {
                Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WorksController.Details), "Works", new { id = comment.WorkID });
            }
            return View(comment);
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

            if (IsValidEditor(comment))
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            if (IsValidEditor(comment))
            {
                comment.Deleted = true;
                TryValidateModel(comment);
            }
            else
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WorksController.Details), "Works", new { id = comment.WorkID });
            }
            return BadRequest();

        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.ID == id);
        }

        private bool IsValidEditor(Comment comment)
        {
            return GetUserID() == comment.UserID;
        }
    }
}
