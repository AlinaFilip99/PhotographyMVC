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
using Photography.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Photography.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PostsController : Controller
    {
        private readonly PostService postService;
        private readonly AccountService accountService;
        private readonly PhotoService photoService;
        private readonly CommentService commentService;

        public PostsController(PostService postService, AccountService accountService, PhotoService photoService, CommentService commentService)
        {
            this.postService = postService;
            this.accountService = accountService;
            this.photoService = photoService;
            this.commentService = commentService;
        }
        /*
        // GET: Posts
        public IActionResult Index()
        {
            var posts = postService.GetPosts();
            return View(posts);
        }
        */
        public IActionResult Index(string id)
        {
            var posts = postService.GetPostByUserId(id);
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id, string message)
        {
            if (message != null)
            {
                var comm = await commentService.CreateComment(User, message, id);
            }
            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult IncrementLikes(int id)
        {
            var post = postService.GetPostById(id);
            post.Likes = post.Likes + 1;
            if (post == null)
            {
                return NotFound();
            }
            postService.UpdatePost(post);
            return RedirectToAction("Details", new { id = id });
        }

        // GET: Posts/Details/5
        public IActionResult Details(int id)
        {
            var post = postService.GetPostById(id);
            var photos = photoService.GetPhotoByPostId(id);
            var comments = commentService.GetCommentByPostId(id);
            if (post == null)
            {
                return NotFound();
            }
            var PPCViewModel = new PostPhotosCommentsViewModel
            {
                post = post,
                photos = photos,
                comments = comments
            };
            return View(PPCViewModel);
        }

        public IActionResult DetailsProfile(int id)
        {
            var post = postService.GetPostById(id);
            var photos = photoService.GetPhotoByPostId(id);
            var comments = commentService.GetCommentByPostId(id);
            if (post == null)
            {
                return NotFound();
            }
            var PPCViewModel = new PostPhotosCommentsViewModel
            {
                post = post,
                photos = photos,
                comments = comments
            };
            return View(PPCViewModel);
        }
        public IActionResult DeleteComm(int id, int postid)
        {
            commentService.RemoveComment(id);
            return RedirectToAction("Details", new { id = postid });
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostPhotosCreate postPhotosCreate)
        {
            if (ModelState.IsValid)
            {
                var post = postService.CreatePostWithDesc(User, postPhotosCreate.description);
                foreach (var file in postPhotosCreate.pictureFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    _=photoService.AddPhoto("~/Images/" + fileName, post.Id);
                    
                    fileName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);

                    using (Stream fileStream = new FileStream(fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                //postService.AddPost(post);
                return RedirectToAction("Profile", "Accounts");
            }
            //ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", post.AccountId);
            return View("Accounts", "Profile");
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
            postService.RemovePost(id);
            return RedirectToAction("Profile", "Accounts");
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
