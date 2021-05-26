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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Photography.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class AccountsController : Controller
    {
        private readonly AccountService accountService;
        private readonly PostService postService;
        private readonly PhotoService photoService;

        public AccountsController(AccountService AccountService, PostService postService, PhotoService photoService)
        {
            //_context = context;
            this.accountService = AccountService;
            this.postService = postService;
            this.photoService = photoService;
            //this.roleService = roleService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                string fileName = Path.GetFileNameWithoutExtension(model.ProfilePictureFile.FileName);
                string extension = Path.GetExtension(model.ProfilePictureFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                var result = await accountService.RegisterUser(model.Nume, model.Prenume, model.Email,
                    "~/Images/" + fileName, model.Password);
                fileName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);

                using (Stream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await model.ProfilePictureFile.CopyToAsync(fileStream);
                }

                if (result.Succeeded)
                {
                    TempData["message"] = "User created succesfully!";
                    return RedirectToAction("Index", "Home");
                }

                //_logger.LogInformation("Unable to register user.");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.SignInCredentials(user.Email, user.Password, user.RememberMe);
                
                if (result.Succeeded)
                {
                    TempData["message"] = "Logged in succesfully!";
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        public IActionResult Logout()
        {
            accountService.SignOut();
            TempData["message"] = "User logged out!";
            return RedirectToAction("Index", "Home");
        }

        // GET: Accounts
        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PrenumeSortParm = sortOrder == "pre_asc" ? "pre_desc" : "pre_asc";

            IEnumerable<Account> accounts = accountService.GetAccounts();

            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accountService.GetAccountsByName(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    accounts = accounts.OrderByDescending(s => s.Nume);
                    break;
                case "pre_asc":
                    accounts = accounts.OrderBy(s => s.Prenume);
                    break;
                case "pre_desc":
                    accounts = accounts.OrderByDescending(s => s.Prenume);
                    break;
                default:
                    accounts = accounts.OrderBy(s => s.Nume);
                    break;
            }
            return View(accounts);
        }
        [Authorize(Roles ="Admin")]
        // GET: Accounts/Details/5
        public IActionResult Details(string id)
        {

            Account account = accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        public async Task<IActionResult> Profile(string? id)
        {
            Account account;
            IEnumerable<int> postsIds= new List<int>().AsEnumerable();
            IEnumerable<Photo> photos = new List<Photo>().AsEnumerable();
            if (id != null)
            {
                account = await accountService.GetAccountByName(id);
                postsIds = postService.GetPostIdByUserId(account.Id);
                photos = photoService.GetPhotosByPostIds(postsIds);
            }
            else
            {
                account = await accountService.GetUser(User);
                postsIds = postService.GetPostIdByUserId(account.Id);
                photos = photoService.GetPhotosByPostIds(postsIds);
            }

            var accountVM = new AccountPhotos
            {
                account = account,
                photos= photos,
            };

            if (account == null)
            {
                return NotFound();
            }

            return View(accountVM);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            //ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id");
            return View();
        }
        
        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountViewModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(accountViewModel.ProfilePictureFile.FileName);
                string extension = Path.GetExtension(accountViewModel.ProfilePictureFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                accountViewModel.account.ProfilePicture = "~/Images/" + fileName;
                fileName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                using (Stream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await accountViewModel.ProfilePictureFile.CopyToAsync(fileStream);
                }
                //_context.Add(accountViewModel.account);
                accountService.AddAccount(accountViewModel.account);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id", accountViewModel.account.);
            return View(accountViewModel.account);
        }*/

        // GET: Accounts/Edit/5
        public IActionResult Edit(string id)
        {
            var account = accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountVM = new AccountViewModel
            {
                userName=account.UserName,
                nume=account.Nume,
                prenume=account.Prenume,
                email=account.Email,
                phoneNumber=account.PhoneNumber,
                facebook=account.FacebookLink,
                instagram=account.InstagramLink,
                twitter=account.TwitterLink
            };
            //ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id", account.RoleId);
            return View(accountVM);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountViewModel accountViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if(accountViewModel.ProfilePictureFile != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(accountViewModel.ProfilePictureFile.FileName);
                        string extension = Path.GetExtension(accountViewModel.ProfilePictureFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        await accountService.UpdateAccount(User, accountViewModel.userName, accountViewModel.nume, accountViewModel.prenume, accountViewModel.email,
                            accountViewModel.phoneNumber, "~/Images/" + fileName, accountViewModel.currentPassword, accountViewModel.newPassword,
                            accountViewModel.facebook, accountViewModel.instagram, accountViewModel.twitter);
                        fileName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                        using (Stream fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            await accountViewModel.ProfilePictureFile.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        await accountService.UpdateAccount(User, accountViewModel.userName, accountViewModel.nume, accountViewModel.prenume, accountViewModel.email,
                            accountViewModel.phoneNumber, " ", accountViewModel.currentPassword, accountViewModel.newPassword,
                            accountViewModel.facebook, accountViewModel.instagram, accountViewModel.twitter);
                    }
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction(nameof(Profile));
            }
            //ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id", accountViewModel.account.RoleId);
            return View();
        }

        // GET: Accounts/Delete/5
        public IActionResult Delete(string id)
        {

            Account account = accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            accountService.RemoveAccount(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return accountService.CheckAccount(id);
        }
    }
  
}
