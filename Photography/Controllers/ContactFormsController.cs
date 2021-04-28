﻿using System;
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
    public class ContactFormsController : Controller
    {
        //private readonly PhotographyContext _context;
        private readonly ContactFormService contactService;
        private readonly AccountService accountService;

        public ContactFormsController(ContactFormService contactService, AccountService accountService)
        {
            this.contactService = contactService;
            this.accountService = accountService;
        }

        // GET: ContactForms
        public IActionResult Index()
        {
            var contacts = contactService.GetContactForms();
            return View(contacts);
        }

        // GET: ContactForms/Details/5
        public IActionResult Details(int id)
        {

            var contactForm = contactService.GetContactFormById(id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // GET: ContactForms/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id");
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NumeF,PrenumeF,DataF,Message,AccountId")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                contactService.AddContactForm(contactForm);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", contactForm.AccountId);
            return View(contactForm);
        }

        // GET: ContactForms/Edit/5
        public IActionResult Edit(int id)
        {
            var contactForm = contactService.GetContactFormById(id);
            if (contactForm == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", contactForm.AccountId);
            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,NumeF,PrenumeF,DataF,Message,AccountId")] ContactForm contactForm)
        {
            if (id != contactForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contactService.UpdateContactForm(contactForm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactFormExists(contactForm.Id))
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
            ViewData["AccountId"] = new SelectList(accountService.GetAccounts(), "Id", "Id", contactForm.AccountId);
            return View(contactForm);
        }

        // GET: ContactForms/Delete/5
        public IActionResult Delete(int id)
        {

            var contactForm = contactService.GetContactFormById(id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            contactService.RemoveContactForm(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
            return contactService.CheckContactForm(id);
        }
    }
}
