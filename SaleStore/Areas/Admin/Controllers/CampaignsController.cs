﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleStore.Data;
using SaleStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]

    public class CampaignsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment env;

        public CampaignsController(IHostingEnvironment _env, ApplicationDbContext context)
        {
            _context = context;
            this.env = _env;
        }

        // GET: Admin/Campaigns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Campaigns.Include(c => c.Category).Include(c => c.Companies);
            ViewBag.Companies = _context.Companies;
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SelectCompany(string selectCompany)
        {
            var applicationDbContext = _context.Campaigns.Include(c => c.Category).Include(c => c.Companies).Where(sc=>sc.Companies.Name==selectCompany);
            ViewBag.Companies = _context.Companies.Where(a=>a.Name!=selectCompany);
            ViewBag.SelectedCompany = _context.Companies.Where(sc => sc.Name == selectCompany).FirstOrDefault().Name;
            return View("index", await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.Category)
                .Include(c=>c.Companies)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Admin/Campaigns/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: Admin/Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Campaign campaign, IFormFile uploadFile)
        {
            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    campaign.CreatedBy = User.Identity.Name ?? "username";
                    campaign.CreateDate = DateTime.Now;
                    campaign.UpdatedBy = User.Identity.Name ?? "username";
                    campaign.UpdateDate = DateTime.Now;

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-CampaignImages";
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        campaign.Image = "uploads/" + category + "/" + dosyaismi;
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                await uploadFile.CopyToAsync(stream);
                            }

                            _context.Add(campaign);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("Image", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", campaign.CategoryId);
                ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View(campaign);
        }

        // GET: Admin/Campaigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns.SingleOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", campaign.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View(campaign);
        }

        // POST: Admin/Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Slogan,CategoryId,CampaignStartDate,CampaignEndDate,Image,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate,IsPublish,CompanyId,SelectedCampaign")] Campaign campaign,IFormFile uploadFile)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            campaign.UpdatedBy = User.Identity.Name ?? "username";
            campaign.UpdateDate = DateTime.Now;

            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {
                

                if (uploadFile != null)
                {

                    

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-CampaignImages";
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        campaign.Image = "uploads/" + category + "/" + dosyaismi;
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                await uploadFile.CopyToAsync(stream);
                            }

                            _context.Update(campaign);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("Image", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else
                {

                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
          
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", campaign.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View(campaign);
        }

        // GET: Admin/Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.Category)
                .Include(c=>c.Companies)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Admin/Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _context.Campaigns.SingleOrDefaultAsync(m => m.Id == id);
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.Id == id);
        }
    }
}
