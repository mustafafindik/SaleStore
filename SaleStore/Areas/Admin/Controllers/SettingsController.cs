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
    public class SettingsController : Controller
    {
        private Data.ApplicationDbContext _context;
        private IHostingEnvironment env;
        public SettingsController(IHostingEnvironment _env, Data.ApplicationDbContext context)
        {
            this._context = context;
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
        public IActionResult Index(Setting setting, IFormFile logoUpload)
        {
            if (ModelState.IsValid)
            {
                Setting s;
                if (_context.Setting.Any())
                {
                    s = _context.Setting.FirstOrDefault();

                    s.Logo = setting.Logo;
                    s.Phone = setting.Phone;
                    s.Address = setting.Address;
                    s.Mail = setting.Mail;
                    s.About = setting.About;
                    s.Title = setting.Title;
                    s.SeoTitle = setting.SeoTitle;
                    s.SeoDescription = setting.SeoDescription;
                    s.SeoKeywords = setting.SeoKeywords;

                    s.Facebook = setting.Facebook;
                    s.Twitter = setting.Twitter;
                    s.LinkedIn = setting.LinkedIn;


                    if (logoUpload != null && logoUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + logoUpload.FileName;
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\" + filePath, FileMode.Create))
                        {
                            logoUpload.CopyTo(stream);
                        }
                        s.Logo = filePath;
                    }
                    _context.SaveChanges();
                }
                else
                {
                    // file upload iþlemi yapýlýr
                    if (logoUpload != null && logoUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + logoUpload.FileName;
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\" + filePath, FileMode.Create))
                        {
                            logoUpload.CopyTo(stream);
                        }
                        setting.Logo = filePath;
                    }
                    _context.Setting.Add(setting);
                    _context.SaveChanges();
                    ViewBag.Message = "Ayarlar baþarýyla kaydedildi.";
                }


            }
            return View(setting);
        }
    }
}
           