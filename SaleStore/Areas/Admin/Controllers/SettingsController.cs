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

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : ControllerBaseAdmin
    {
        private IHostingEnvironment env;
        public SettingsController(IHostingEnvironment _env, Data.ApplicationDbContext context) :base(context)
        {
            this.env = _env;
        }
        // GET: Admin/Settings
        public IActionResult Index()
        {
            Setting setting;
            setting = _context.Setting.FirstOrDefault();
            //setting = _context.Setting.FirstOrDefault();
            if (setting == null)
            {
                setting = new Setting();
            }
            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Setting setting, IFormFile logoUpload)
        {
            setting.CreatedBy = User.Identity.Name ?? "username";
            setting.CreateDate = DateTime.Now;
            setting.UpdatedBy = User.Identity.Name ?? "username";
            setting.UpdateDate = DateTime.Now;
            if (logoUpload != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(logoUpload.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {

                if (logoUpload != null)
                {

                    if (Path.GetExtension(logoUpload.FileName) == ".jpg"
                    || Path.GetExtension(logoUpload.FileName) == ".gif"
                    || Path.GetExtension(logoUpload.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-Logos";
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(logoUpload.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        setting.Logo = "/uploads/" + category + "/" + dosyaismi;
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                await logoUpload.CopyToAsync(stream);
                            }

                            if (_context.Setting != null)
                            {
                                _context.Update(setting);
                                await _context.SaveChangesAsync();
                                ViewBag.Message = "Ayarlarınız Başarıyla kaydedildi.";
                            }
                            else
                            {
                                _context.Add(setting);
                                await _context.SaveChangesAsync();
                                ViewBag.Message = "Ayarlarınız Başarıyla kaydedildi.";
                            }
                            return View(setting);
                        }
                        catch (Exception exc) { ModelState.AddModelError("ProductImage", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("ProductImage", "Dosya uzantýsý izin verilen uzantılardan olmalıdır.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
            _context.Update(setting);
            await _context.SaveChangesAsync();
            ViewBag.Message = "Ayarlarınız Başarıyla kaydedildi.";
            return View(setting);
        }
    }
}
           