using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleStore.Data;
using SaleStore.Models;

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InboxesController : ControllerBaseAdmin
    {

        public InboxesController(ApplicationDbContext context) : base(context)
        {  
        }

        // GET: Admin/Inboxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inboxes.ToListAsync());
        }

        // GET: Admin/Inboxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbox = await _context.Inboxes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inbox == null)
            {
                return NotFound();
            }

            return View(inbox);
        }

        // GET: Admin/Inboxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Inboxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Phone,Email,Message,SubmitDate,Ip")] Inbox inbox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inbox);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inbox);
        }

        // GET: Admin/Inboxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbox = await _context.Inboxes.SingleOrDefaultAsync(m => m.Id == id);
            if (inbox == null)
            {
                return NotFound();
            }
            return View(inbox);
        }

        // POST: Admin/Inboxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Phone,Email,Message,SubmitDate,Ip")] Inbox inbox)
        {
            if (id != inbox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inbox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InboxExists(inbox.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(inbox);
        }

        // GET: Admin/Inboxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbox = await _context.Inboxes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inbox == null)
            {
                return NotFound();
            }

            return View(inbox);
        }

        // POST: Admin/Inboxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inbox = await _context.Inboxes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Inboxes.Remove(inbox);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InboxExists(int id)
        {
            return _context.Inboxes.Any(e => e.Id == id);
        }
    }
}
