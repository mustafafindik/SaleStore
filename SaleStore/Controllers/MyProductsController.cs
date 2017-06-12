using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleStore.Data;
using SaleStore.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SaleStore.Models.ViewModels;
using PagedList.Core;

namespace SaleStore.Controllers
{
    public class MyProductsController : ControllerBase
    {
        private IHostingEnvironment env;
        HomePageViewModels model = new HomePageViewModels();

        public MyProductsController(IHostingEnvironment _env, ApplicationDbContext context) :base(context)
        {
            this.env = _env;
        }

        // GET: Admin/Products
        public IActionResult Index(int page = 1)
        { 
            model.Categories = _context.Categories.ToList();
            model.Campaigns = _context.Campaigns.ToPagedList<Campaign>(page, 9);
            model.Products = _context.Products.ToPagedList<Product>(page, 9);
            return View(model);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile uploadFile)
        {
            if(product.SalePrice > product.UnitPrice)
            {
                ModelState.AddModelError("SalePrice","İndirimli fiyat birim fiyattan yüksek olamaz");
            }
            if(product.SaleStarthDate > product.SaleEndDate)
            {
                ModelState.AddModelError("SaleEndDate","İndirim bitiş tarihi indirim başlangıç tarihinden erken olamaz");
            }

            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {

                    product.CreatedBy = User.Identity.Name ?? "username";
                    product.CreateDate = DateTime.Now;
                    product.UpdatedBy = User.Identity.Name ?? "username";
                    product.UpdateDate = DateTime.Now;

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "ProductImages";
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        product.ProductImage = "uploads/" + category + "/"+dosyaismi;
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


                            _context.Add(product);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("ProductImage", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("ProductImage", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", product.CompanyId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", product.CompanyId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Details,UnitPrice,SalePrice,SaleStarthDate,SaleEndDate,CategoryId,ProductImage,CompanyId,Id,CreateDate,CreatedBy,UpdatedBy,UpdateDate,IsPublished")] Product product,IFormFile uploadFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (product.SalePrice > product.UnitPrice)
            {
                ModelState.AddModelError("SalePrice", "İndirimli fiyat birim fiyattan yüksek olamaz");
            }
            if (product.SaleStarthDate > product.SaleEndDate)
            {
                ModelState.AddModelError("SaleEndDate", "İndirim bitiş tarihi indirim başlangıç tarihinden erken olamaz");
            }
            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }

            if (ModelState.IsValid)
            {

                if (uploadFile != null)
                {

                    product.UpdatedBy = User.Identity.Name ?? "username";
                    product.UpdateDate = DateTime.Now;

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-ProductImages";
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        product.ProductImage = "uploads/" + category + "/" + dosyaismi;
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

                            _context.Update(product);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("Image", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("ProductImage", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else
                {
                    
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", product.CompanyId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }    
    }
}
