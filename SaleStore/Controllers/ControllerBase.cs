using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SaleStore.Data;
using SaleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Controllers
{
    public class ControllerBase : Controller
    {
        protected ApplicationDbContext _context;
        public ControllerBase(ApplicationDbContext context)
        {
            this._context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_context.Setting.Any())
            {
                var setting = _context.Setting.FirstOrDefault();
                ViewBag.logo = setting.Logo;
                ViewBag.Title = setting.Title;
                ViewBag.phone = setting.Phone;
                ViewBag.SeoDescription = setting.SeoDescription;
                ViewBag.SeoKeywords = setting.SeoKeywords;
                ViewBag.SeoTitle = setting.SeoTitle;
                ViewBag.Adress = setting.Address;
                ViewBag.Mail = setting.Mail;
                ViewBag.SiteSlogan = setting.SiteSlogan;
                ViewBag.About = setting.About;
                ViewBag.facebook = setting.Facebook;
                ViewBag.Twitter = setting.Twitter;
                ViewBag.LinkedIn = setting.LinkedIn;



            }
            else
            {
                var settings = new Setting();
                ViewBag.logo = settings.Logo;
                ViewBag.Title = settings.Title;
                ViewBag.phone = settings.Phone;
                ViewBag.SeoDescription = settings.SeoDescription;
                ViewBag.SeoKeywords = settings.SeoKeywords;
                ViewBag.SeoTitle = settings.SeoTitle;
                ViewBag.Adress = settings.Address;
                ViewBag.Mail = settings.Mail;
                ViewBag.SiteSlogan = settings.SiteSlogan;
                ViewBag.About = settings.About;
                ViewBag.facebook = settings.Facebook;
                ViewBag.Twitter = settings.Twitter;
                ViewBag.LinkedIn = settings.LinkedIn;

            }

        }
    }
}
