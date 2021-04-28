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

namespace Photography.Controllers
{
    public class AccountsController : Controller
    {
        //private readonly PhotographyContext _context;
        private readonly AccountService accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleService roleService;

        public AccountsController(AccountService AccountService, IWebHostEnvironment webHostEnvironment, RoleService roleService)
        {
            //_context = context;
            this.accountService = AccountService;
            this._webHostEnvironment = webHostEnvironment;
            this.roleService = roleService;
        }

        // GET: Accounts
        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PrenumeSortParm = sortOrder == "pre_asc" ? "pre_desc" : "pre_asc";

            var accounts = accountService.GetAccounts();

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
        
        // GET: Accounts/Details/5
        public IActionResult Details(int id)
        {

            var account = accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        
        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id");
            return View();
        }
        
        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
            ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id", accountViewModel.account.RoleId);
            return View(accountViewModel.account);
        }

        // GET: Accounts/Edit/5
        public IActionResult Edit(int id)
        {
            var account = accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id", account.RoleId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccountViewModel accountViewModel)
        {
            if (id != accountViewModel.account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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
                    accountService.UpdateAccount(accountViewModel.account);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(accountViewModel.account.Id))
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
            ViewData["RoleId"] = new SelectList(roleService.GetRoles(), "Id", "Id", accountViewModel.account.RoleId);
            return View(accountViewModel.account);
        }

        // GET: Accounts/Delete/5
        public IActionResult Delete(int id)
        {

            var account = accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            accountService.RemoveAccount(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return accountService.CheckAccount(id);
        }
    }
  
}
