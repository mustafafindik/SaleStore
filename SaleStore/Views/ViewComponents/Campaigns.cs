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
    public class Campaigns : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Campaigns(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryNames)
        {

            var items = GetItems(categoryNames).AsEnumerable();

            return View(items);
        }
        public List<Campaign> GetItems(string categoryNames)
        {
            if (categoryNames != null)
            {
                return GetCampaignsByCategoryNames(categoryNames, 4).Where(w => w.IsPublish == true).ToList();
            }
            else
            {
                return GetCampaigns().Where(w => w.IsPublish == true).ToList();
            }
        }
        public IEnumerable<Campaign> GetCampaigns()
        {
            var campaigns = GetAll().OrderByDescending(p => p.CreateDate);
            return campaigns;
        }
        public virtual IEnumerable<Campaign> GetAll(params string[] navigations)
        {
            var set = _context.Campaigns.AsQueryable();
            foreach (string nav in navigations)
                set = set.Include(nav);
            return set.AsEnumerable();
        }
        public IEnumerable<Campaign> GetCampaignsByCategoryNames(string categoryNames, int count)
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
            var campaigns = GetCampaignsByCategoryNames(categories, count);
            return campaigns;
        }


        public IEnumerable<Campaign> GetCampaignsByCategoryNames(string[] categories, int count)
        {
            if (categories.Length > 0)
            {
                return (from p in _context.Campaigns join pc in _context.Categories on p.CategoryId equals pc.Id where (categories.Length > 0 ? categories.Contains(p.Name.ToLower()) : true) orderby p.CreateDate descending select p).Take(count).ToList();
            }
            else
            {
                return (from p in _context.Campaigns orderby p.CreateDate descending select p).Take(count).ToList();
            }
        }
    }
}