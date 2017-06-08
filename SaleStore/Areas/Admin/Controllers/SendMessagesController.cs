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
    public class SendMessagesController : ControllerBaseAdmin
    {

        public SendMessagesController(ApplicationDbContext context) :base(context)
        {    
        }

        // GET: Admin/SendMessages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SendMessages.Include(s => s.MailSetting);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/SendMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendMessage = await _context.SendMessages
                .Include(s => s.MailSetting)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sendMessage == null)
            {
                return NotFound();
            }

            return View(sendMessage);
        }

        // GET: Admin/SendMessages/Create
        public IActionResult Create()
        {
            ViewData["MailSettingId"] = new SelectList(_context.MailSettings, "Id", "FromAddress");
            return View();
        }

        // POST: Admin/SendMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subject,BodyContent,MailSettingId,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate")] SendMessage sendMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sendMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MailSettingId"] = new SelectList(_context.MailSettings, "Id", "FromAddress", sendMessage.MailSettingId);
            return View(sendMessage);
        }

        // GET: Admin/SendMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendMessage = await _context.SendMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (sendMessage == null)
            {
                return NotFound();
            }
            ViewData["MailSettingId"] = new SelectList(_context.MailSettings, "Id", "FromAddress", sendMessage.MailSettingId);
            return View(sendMessage);
        }

        // POST: Admin/SendMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Subject,BodyContent,MailSettingId,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate")] SendMessage sendMessage)
        {
            if (id != sendMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sendMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SendMessageExists(sendMessage.Id))
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
            ViewData["MailSettingId"] = new SelectList(_context.MailSettings, "Id", "FromAddress", sendMessage.MailSettingId);
            return View(sendMessage);
        }

        // GET: Admin/SendMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sendMessage = await _context.SendMessages
                .Include(s => s.MailSetting)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sendMessage == null)
            {
                return NotFound();
            }

            return View(sendMessage);
        }

        // POST: Admin/SendMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sendMessage = await _context.SendMessages.SingleOrDefaultAsync(m => m.Id == id);
            _context.SendMessages.Remove(sendMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SendMessageExists(int id)
        {
            return _context.SendMessages.Any(e => e.Id == id);
        }
    }
}
