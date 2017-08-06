using System;
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
using SaleStore.Models.ViewModels;
using PagedList.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SaleStore.Controllers
{
    [Authorize]

    public class MyCampaignsController : ControllerBase
    {
        private IHostingEnvironment env;
        HomePageViewModels model = new HomePageViewModels();
        private readonly UserManager<ApplicationUser> _userManager;

        public MyCampaignsController(IHostingEnvironment _env, ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            this.env = _env;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Admin/Products
        public async Task<IActionResult> Index(int page = 1)
        {
            Company company = new Company();
            string CurrentUserId = await GetCurrentUserId();
            model.Companies = _context.Companies.Where(x => x.UserId == CurrentUserId).ToList();

            model.Categories = _context.Categories.ToList();
            model.Products = _context.Products.ToPagedList<Product>(page, 9);
            List<Company> companies = _context.Companies.ToList();
            model.Campaigns = _context.Campaigns.Where(x => x.CompanyId == companies.Where(y => y.UserId == CurrentUserId).FirstOrDefault().Id).ToPagedList<Campaign>(page, 100000);
            return View(model);
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
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Admin/Campaigns/Create
        public async Task<IActionResult> Create(int page = 1)
        {
            Company company = new Company();
            List<Company> companies = _context.Companies.ToList();
            string CurrentUserId = await GetCurrentUserId();
            model.Companies = _context.Companies.Where(x => x.UserId == CurrentUserId).ToList();
            model.Campaigns = _context.Campaigns.Where(x => x.CompanyId == companies.Where(y => y.UserId == CurrentUserId).FirstOrDefault().Id).ToPagedList<Campaign>(page, 100000);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Companies.Where(x => x.UserId == CurrentUserId), "Id", "Name");
            return View();

            }

        // POST: Admin/Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Campaign campaign, IFormFile uploadFile, int page =1)
        {
            Company company = new Company();
            List<Company> companies = _context.Companies.ToList();
            string CurrentUserId = await GetCurrentUserId();
            model.Companies = _context.Companies.Where(x => x.UserId == CurrentUserId).ToList();
            model.Campaigns = _context.Campaigns.Where(x => x.CompanyId == companies.Where(y => y.UserId == CurrentUserId).FirstOrDefault().Id).ToPagedList<Campaign>(page, 100000);

            if (campaign.CampaignStartDate > campaign.CampaignEndDate)
            {
                ModelState.AddModelError("CampaignEndDate", "Kampanya bitiş tarihi kampanya başlangıç tarihinden erken olamaz");
            }
            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid && model.Campaigns.Count() < model.Companies.FirstOrDefault().CampaignCount)
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

            campaign.CompanyId = _context.Companies.Where(x => x.UserId == CurrentUserId).FirstOrDefault().Id;
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", campaign.CompanyId);
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
            string CurrentUserId = await GetCurrentUserId();
            ViewData["CompanyId"] = new SelectList(_context.Companies.Where(x => x.UserId == CurrentUserId), "Id", "Name");
            return View(campaign);
        }

        // POST: Admin/Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Slogan,Description,CategoryId,CampaignStartDate,CampaignEndDate,Image,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate,IsPublish,CompanyId")] Campaign campaign, IFormFile uploadFile)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }
            if (campaign.CampaignStartDate > campaign.CampaignEndDate)
            {
                ModelState.AddModelError("CampaignEndDate", "Kampanya bitiş tarihi kampanya başlangıç tarihinden erken olamaz");
            }

            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {

                if (uploadFile != null)
                {

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
            string CurrentUserId = await GetCurrentUserId();
            campaign.CompanyId = _context.Companies.Where(x => x.UserId == CurrentUserId).FirstOrDefault().Id;
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", campaign.CompanyId);
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
