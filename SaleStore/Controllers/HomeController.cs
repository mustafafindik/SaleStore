using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaleStore.Models.ViewModels;
using SaleStore.Data;
using SaleStore.Models;

namespace SaleStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        HomePageViewModels model = new HomePageViewModels();
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            
            model.Categories = _context.Categories.ToList();
            model.Campaigns = _context.Campaigns.ToList();
            model.Products = _context.Products.ToList();
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
