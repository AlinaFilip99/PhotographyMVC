using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Photography.DataAccess;
using Photography.ApplicationLogic.Models;

namespace Photography.Controllers
{
    public class ContactFormsController : Controller
    {
        private readonly PhotographyContext _context;

        public ContactFormsController(PhotographyContext context)
        {
            _context = context;
        }

        // GET: ContactForms
        public async Task<IActionResult> Index()
        {
            var photographyContext = _context.ContactForms.Include(c => c.Account);
            return View(await photographyContext.ToListAsync());
        }

        // GET: ContactForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForms
                .Include(c => c.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // GET: ContactForms/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeF,PrenumeF,DataF,Message,AccountId")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", contactForm.AccountId);
            return View(contactForm);
        }

        // GET: ContactForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForms.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", contactForm.AccountId);
            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeF,PrenumeF,DataF,Message,AccountId")] ContactForm contactForm)
        {
            if (id != contactForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactForm);
                    await _context.SaveChangesAsync();
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", contactForm.AccountId);
            return View(contactForm);
        }

        // GET: ContactForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForms
                .Include(c => c.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactForm = await _context.ContactForms.FindAsync(id);
            _context.ContactForms.Remove(contactForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
            return _context.ContactForms.Any(e => e.Id == id);
        }
    }
}
