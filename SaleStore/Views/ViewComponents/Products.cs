using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SaleStore.Data;
using SaleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Views.ViewComponents
{
    public class Products : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Products(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryNames)
        {
   
            var items = GetItems(categoryNames).AsEnumerable();
     
            return View(items);
        }
        public List<Product> GetItems(string categoryNames)
        {
            if (categoryNames != null)
            {
                return GetProductsByCategoryNames(categoryNames, 4).Where(w => w.IsPublish == true).ToList();
            }
            else
            {
                return GetProducts().Where(w => w.IsPublish == true).ToList();
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            var products = GetAll().OrderByDescending(p => p.CreateDate);
            return products;
        }
        public virtual IEnumerable<Product> GetAll(params string[] navigations)
        {
            var set = _context.Products.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.AsEnumerable();
        }
        public IEnumerable<Product> GetProductsByCategoryNames(string categoryNames, int count)
        {
            string[] categories;
            if (categoryNames == "")
            {
                categories = new string[0];
            }
            else
            {
                categories = categoryNames.Split(',');
            }

            for (var i = 0; i < categories.Length; i++)
            {
                categories[i] = categories[i].Trim().ToLower();
            }
            var products = GetProductsByCategoryNames(categories, count);
            return products;
        }


        public IEnumerable<Product> GetProductsByCategoryNames(string[] categories, int count)
        {
            if (categories.Length > 0)
            {
                return (from p in _context.Products join pc in _context.Categories on p.CategoryId equals pc.Id  where (categories.Length > 0 ? categories.Contains(p.Name.ToLower()) : true) orderby p.CreateDate descending select p).Take(count).ToList();
            }
            else
            {
                return (from p in _context.Products orderby p.CreateDate descending select p).Take(count).ToList();
            }
        }
    }
}