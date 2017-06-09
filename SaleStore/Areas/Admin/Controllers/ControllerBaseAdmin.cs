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
            

        }
    }
}
