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
    public class MailSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MailSettingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin/MailSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.MailSettings.ToListAsync());
        }

        // GET: Admin/MailSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailSetting = await _context.MailSettings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mailSetting == null)
            {
                return NotFound();
            }

            return View(mailSetting);
        }

        // GET: Admin/MailSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MailSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FromAddress,FromAddressPassword,FromAddressTitle,SmptServer,SmptPortNumber,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate")] MailSetting mailSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mailSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mailSetting);
        }

        // GET: Admin/MailSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailSetting = await _context.MailSettings.SingleOrDefaultAsync(m => m.Id == id);
            if (mailSetting == null)
            {
                return NotFound();
            }
            return View(mailSetting);
        }

        // POST: Admin/MailSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FromAddress,FromAddressPassword,FromAddressTitle,SmptServer,SmptPortNumber,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate")] MailSetting mailSetting)
        {
            if (id != mailSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mailSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MailSettingExists(mailSetting.Id))
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
            return View(mailSetting);
        }

        // GET: Admin/MailSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailSetting = await _context.MailSettings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mailSetting == null)
            {
                return NotFound();
            }

            return View(mailSetting);
        }

        // POST: Admin/MailSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mailSetting = await _context.MailSettings.SingleOrDefaultAsync(m => m.Id == id);
            _context.MailSettings.Remove(mailSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MailSettingExists(int id)
        {
            return _context.MailSettings.Any(e => e.Id == id);
        }
    }
}
