using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleStore.Data;
using SaleStore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class AdvertisementsController : ControllerBaseAdmin
    {
        private IHostingEnvironment env;

        public AdvertisementsController(ApplicationDbContext context, IHostingEnvironment _env) : base(context)
        {
            this.env = _env;    
        }

        // GET: Admin/Advertisements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertisements.ToListAsync());
        }

        // GET: Admin/Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .SingleOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Admin/Advertisements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Title,Description,advType,HomePageCampaigns,HomePageProducts,ProductsPage,CampaignPage,HTMLCodes,URLpath,Image,Ispublished,AdvertiseStartDate,AdvertiseEndDate,ClickRate,PositionDegree,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate")] Advertisement advertisement,IFormFile uploadFile)
        {
            advertisement.CreatedBy = User.Identity.Name ?? "username";
            advertisement.CreateDate = DateTime.Now;
            advertisement.UpdatedBy = User.Identity.Name ?? "username";
            advertisement.UpdateDate = DateTime.Now;
            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("Image", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {



                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-AdvertisementImages";
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        advertisement.Image = "uploads/" + category + "/" + dosyaismi;
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


                            _context.Add(advertisement);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("Image", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Dosya uzantýsý izin verilen uzantýlardan olmalýdýr.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
            
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.SingleOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Admin/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Title,Description,advType,HomePageCampaigns,HomePageProducts,ProductsPage,CampaignPage,HTMLCodes,URLpath,Image,Ispublished,AdvertiseStartDate,AdvertiseEndDate,ClickRate,PositionDegree,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .SingleOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Admin/Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.SingleOrDefaultAsync(m => m.Id == id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
