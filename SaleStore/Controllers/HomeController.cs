using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaleStore.Models.ViewModels;
using SaleStore.Data;

namespace SaleStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            HomePageViewModels model = new HomePageViewModels();
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
    }
}
