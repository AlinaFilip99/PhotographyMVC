using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Photography.DataAccess;
using Photography.ApplicationLogic.Models;
using Photography.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Photography.ApplicationLogic.Services;

namespace Photography.Controllers
{
    public class PhotosController : Controller
    {
        private readonly PhotoService photoService;
        private readonly PostService postService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotosController(PhotoService photoService, IWebHostEnvironment webHostEnvironment, PostService postService)
        {
            this.photoService = photoService;
            this._webHostEnvironment = webHostEnvironment;
            this.postService = postService;
        }

        // GET: Photos
        public IActionResult Index()
        {
            var photos = photoService.GetPhotos();
            return View(photos);
        }

        // GET: Photos/Details/5
        public IActionResult Details(int id)
        {
            var photo = photoService.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id");
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(photoViewModel.PictureFile.FileName);
                string extension = Path.GetExtension(photoViewModel.PictureFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                photoViewModel.photo.Picture = "~/Images/" + fileName;
                fileName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                using (Stream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await photoViewModel.PictureFile.CopyToAsync(fileStream);
                }
                photoService.AddPhoto(photoViewModel.photo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id", photoViewModel.photo.PostId);
            return View(photoViewModel.photo);
        }

        // GET: Photos/Edit/5
        public IActionResult Edit(int id)
        {
            var photo = photoService.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound();
            }
            var photoview = new PhotoViewModel
            {
                photo = photo
            };
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id", photo.PostId);
            return View(photoview);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhotoViewModel photoViewModel)
        {
            if (id != photoViewModel.photo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(photoViewModel.PictureFile.FileName);
                    string extension = Path.GetExtension(photoViewModel.PictureFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    photoViewModel.photo.Picture = "~/Images/" + fileName;
                    fileName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                    using (Stream fileStream = new FileStream(fileName, FileMode.Create))
                    {
                        await photoViewModel.PictureFile.CopyToAsync(fileStream);
                    }
                    photoService.UpdatePhoto(photoViewModel.photo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photoViewModel.photo.Id))
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
            ViewData["PostId"] = new SelectList(postService.GetPosts(), "Id", "Id", photoViewModel.photo.PostId);
            return View(photoViewModel);
        }

        // GET: Photos/Delete/5
        public IActionResult Delete(int id)
        {
            var photo = photoService.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            photoService.RemovePhoto(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoExists(int id)
        {
            return photoService.CheckPhoto(id);
        }
    }
}
