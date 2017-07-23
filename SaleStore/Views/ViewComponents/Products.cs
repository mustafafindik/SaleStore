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

        public IViewComponentResult Invoke(Category category)
        {

            var items = GetItems(category).AsEnumerable();

            return View(items);
        }
        public List<Product> GetItems(Category category)
        {
            if (category != null)
            {
                return GetProductsByCategoryNames(category).ToList();
            }
            else
            {
                return GetProducts().ToList();
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


        public IEnumerable<Product> GetProductsByCategoryNames(Category category)
        {
            if (category != null)
            {
                return (from p in _context.Products where (p.CategoryId==category.Id)  orderby p.CreateDate descending select p).ToList();
            }
            else
            {
                return (from p in _context.Products orderby p.CreateDate descending select p).ToList();
            }
        }
    }
}