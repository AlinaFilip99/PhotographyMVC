using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Photography.DataAccess;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Services;

namespace Photography.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostService postService;
        private readonly AccountService accountService;

        public PostsController(PostService postService, AccountService accountService)
        {
            this.postService = postService;
            this.accountService = accountService;
        }

        // GET: Posts
        public IActionResult Index()
        {
            var posts = postService.GetPosts();
            return View(posts);
        }

        // GET: Posts/Details/5
        public IActionResult Details(int id)
        {
            var post = postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description,Likes,AccountId")] Post post)
        {
            if (ModelState.IsValid)
            {
                postService.AddPost(post);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", post.AccountId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public IActionResult Edit(int id)
        {
            var post = postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", post.AccountId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,Likes,AccountId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    postService.UpdatePost(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", post.AccountId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public IActionResult Delete(int id)
        {
            var post = postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            postService.RemovePost(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return postService.CheckPost(id);
        }
    }
}
