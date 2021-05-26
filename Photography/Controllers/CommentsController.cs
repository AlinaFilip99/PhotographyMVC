using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Photography.DataAccess;
using Photography.ApplicationLogic.Services;
using Photography.ApplicationLogic.Models;
using Microsoft.AspNetCore.Authorization;

namespace Photography.Controllers
{
    [Authorize(Roles ="Admin,User")]
    public class CommentsController : Controller
    {
        //private readonly PhotographyContext _context;
        private readonly CommentService commentService;
        private readonly PostService postService;

        public CommentsController(CommentService commentService, PostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        // GET: Comments
        public IActionResult Index()
        {
            var comments = commentService.GetComments();
            return View(comments);
        }

        // GET: Comments/Details/5
        public IActionResult Details(int id)
        {
            var comment = commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CommMessage,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                commentService.AddComment(comment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public IActionResult Edit(int id)
        {

            var comment = commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CommMessage,PostId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commentService.UpdateComment(comment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public IActionResult Delete(int id)
        {

            var comment = commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            commentService.RemoveComment(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return commentService.CheckComment(id);
        }
    }
}
