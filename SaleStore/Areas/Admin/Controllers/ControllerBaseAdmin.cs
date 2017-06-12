using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SaleStore.Data;
using SaleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Areas.Admin.Controllers
{
    public class ControllerBaseAdmin :Controller
    {
        protected ApplicationDbContext _context;
        public ControllerBaseAdmin(ApplicationDbContext context)
        {
            this._context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_context.Inboxes.Any())
            {
                IEnumerable<Inbox> messages = _context.Inboxes.OrderByDescending(x=> x.SubmitDate).Take(3);
                var count = 0;
                if (messages.ElementAtOrDefault(count) != null)
                {
                    ViewBag.FullName = messages.ElementAt(count).FullName;
                    ViewBag.message = messages.ElementAt(count).Message;
                    ViewBag.SubmitDate = messages.ElementAt(count).SubmitDate;
                }
                count++;
                if(messages.ElementAtOrDefault(count) != null)
                {
                    ViewBag.FullName2 = messages.ElementAt(count).FullName;
                    ViewBag.message2 = messages.ElementAt(count).Message;
                    ViewBag.SubmitDate2 = messages.ElementAt(count).SubmitDate;
                }
                count++;
                if (messages.ElementAtOrDefault(count) != null)
                {
                    ViewBag.FullName3 = messages.ElementAt(count).FullName;
                    ViewBag.message3 = messages.ElementAt(count).Message;
                    ViewBag.SubmitDate3 = messages.ElementAt(count).SubmitDate;
                }       
            }
            else
            {
                ViewBag.message = "Hiç mesaj yok.";
            }

        }
    }
}
