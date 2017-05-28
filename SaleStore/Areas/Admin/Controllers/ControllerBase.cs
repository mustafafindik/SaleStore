using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaleStore.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaleStore.Areas.Admin.Controllers
{
    public class ControllerBase : Controller
    {
        protected string AssetsUrl;
        protected string UploadPath;


        public ControllerBase()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appSettings = (IOptions<AppSettings>)this.HttpContext.RequestServices.GetService(typeof(IOptions<AppSettings>));
            this.AssetsUrl = appSettings.Value.AssetsUrl;
            this.UploadPath = appSettings.Value.UploadPath;
            ViewBag.AssetsUrl = this.AssetsUrl;
            ViewBag.UploadPath = this.UploadPath;
            base.OnActionExecuting(filterContext);

        }
    }
}
