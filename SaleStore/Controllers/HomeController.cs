using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaleStore.Models.ViewModels;
using SaleStore.Data;
using SaleStore.Models;
using PagedList.Core;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;

namespace SaleStore.Controllers
{
    public class HomeController : ControllerBase
    {
      
        HomePageViewModels model = new HomePageViewModels();

        public HomeController(ApplicationDbContext context) : base(context)
        {
        }

        public IActionResult Index(int page = 1)
        {

            model.Categories = _context.Categories.ToList();
            model.Campaigns = _context.Campaigns.ToPagedList<Campaign>(page, 10);
            model.Products = _context.Products.ToPagedList<Product>(page, 10);
            model.Settings = _context.Setting.ToList();
            //Setting settings = new Setting();
            //settings = _context.Setting.FirstOrDefault();

            //ViewBag.logo = settings.Logo;
            //ViewBag.Title = settings.Title;
            //ViewBag.phone = settings.Phone;
            //ViewBag.SeoDescription = settings.SeoDescription;
            //ViewBag.SeoKeywords = settings.SeoKeywords;
            //ViewBag.SeoTitle = settings.SeoTitle;
            //ViewBag.Adress = settings.Address;
            //ViewBag.Mail = settings.Mail;
            //ViewBag.SiteSlogan = settings.SiteSlogan;
            //ViewBag.About = settings.About;
            return View(model);
        }

        public IActionResult About()
        {
            Setting settings = new Setting();
            settings = _context.Setting.FirstOrDefault();
            ViewBag.Title =settings.Title + "/Hakkımızda";
            ViewBag.logo = settings.Logo;
            ViewBag.phone = settings.Phone;
            ViewBag.SeoDescription = settings.SeoDescription;
            ViewBag.SeoKeywords = settings.SeoKeywords;
            ViewBag.SeoTitle = settings.SeoTitle;
            ViewBag.Mail = settings.Mail;
            ViewBag.SiteSlogan = settings.SiteSlogan;
            ViewBag.Adress = settings.Address;
            ViewBag.About = settings.About;
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Campaigns(int page=1)
        {
            model.Categories = _context.Categories.ToList(); ;
            model.Campaigns = _context.Campaigns.ToPagedList<Campaign>(page, 10);
            model.Products = _context.Products.ToPagedList<Product>(page, 10);
            model.Settings = _context.Setting.ToList();
            return View(model);
        }

        public async Task <IActionResult> Product(int? id)
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

        public IActionResult Products(int page=1)
        {
            model.Categories = _context.Categories.ToList();
            model.Campaigns = _context.Campaigns.ToPagedList<Campaign>(page, 10);
            model.Products = _context.Products.ToPagedList<Product>(page, 10);
            model.Settings = _context.Setting.ToList();
            return View(model);
        }
        public ActionResult Select(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            model.Categories = _context.Categories.ToList<Category>();
            model.Campaigns = _context.Campaigns.ToPagedList<Campaign>(1, 9);
            if (id == 0)
            {
                model.Products = _context.Products.ToPagedList<Product>(1, 9);      
            }
            else
            {
                model.Products = _context.Products.Where(x => x.CategoryId == id).OrderByDescending(x => x.CreateDate).ToPagedList(1, 9);
            }
            return View("Products",model);
            
        }

        public ActionResult SelectCampaigns(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            model.Products = _context.Products.ToPagedList<Product>(1, 9);
            model.Categories = _context.Categories.ToList<Category>();
            if (id == 0)
            {
                model.Campaigns = _context.Campaigns.ToPagedList<Campaign>(1, 9);
            }
            else
            {
                model.Campaigns = _context.Campaigns.Where(x => x.CategoryId == id).OrderByDescending(x => x.CreateDate).ToPagedList(1, 9);
            }
            return View("Campaigns", model);

        }

        public IActionResult Contact()
        {

            Setting settings = new Setting();
            settings = _context.Setting.FirstOrDefault();
            ViewBag.Title = settings.Title + "/Hakkımızda";
            ViewBag.logo = settings.Logo;
            ViewBag.phone = settings.Phone;
            ViewBag.SeoDescription = settings.SeoDescription;
            ViewBag.SeoKeywords = settings.SeoKeywords;
            ViewBag.SeoTitle = settings.SeoTitle;
            ViewBag.Mail = settings.Mail;
            ViewBag.SiteSlogan = settings.SiteSlogan;
            ViewBag.Adress = settings.Address;
            ViewBag.About = settings.About;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Inbox inbox)
        {
            inbox.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
           
                _context.Add(inbox);
                await _context.SaveChangesAsync();

                MailSetting mailSetting;
                SendMessage sendMessage;
                mailSetting = _context.MailSettings.FirstOrDefault();
                sendMessage = _context.SendMessages.Where(x=>x.MailSettingId==1).FirstOrDefault();
                string FromAddress = mailSetting.FromAddress;
                string FromAddressTitle = mailSetting.FromAddressTitle;

                string ToAddress = inbox.Email;
                string ToAddressTitle = inbox.FullName;
                string Subject = sendMessage.Subject;
                string BodyContent = sendMessage.BodyContent;

                string SmptServer = mailSetting.SmptServer;
                int SmptPortNumber = mailSetting.SmptPortNumber;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAddressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAddressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmptServer, SmptPortNumber, false);
                    client.Authenticate(mailSetting.FromAddress, mailSetting.FromAddressPassword);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
                ViewBag.Message = "Mesajınız başarıyla gönderildi.";

            

            return View(inbox);

        }


        public IActionResult Error()
        {
            return View();
        }
        //public IActionResult LastAddedProducts()
        //{
        //    List<Product> lastProducts;
        //    if (model != null)
        //    {
        //        //lastProducts = _context.Products.OrderByDescending(p => p.UpdateDate).ToList();
        //        lastProducts = model.Products.OrderByDescending(p => p.UpdateDate).ToList();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //    return View("Index", lastProducts);
        //}

    }
    
}
