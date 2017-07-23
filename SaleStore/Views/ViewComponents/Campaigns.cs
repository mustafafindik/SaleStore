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

        public IViewComponentResult Invoke(Category category)
        {

            var items = GetItems(category).AsEnumerable();

            return View(items);
        }
        public List<Campaign> GetItems(Category category)
        {
            if (category != null)
            {
                return GetCampaignsByCategoryNames(category).ToList();
            }
            else
            {
                return GetCampaigns().ToList();
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


        public IEnumerable<Campaign> GetCampaignsByCategoryNames(Category category)
        {
            if (category != null)
            {
                return (from c in _context.Campaigns where (c.CategoryId == category.Id) orderby c.CreateDate descending select c).ToList();
            }
            else
            {
                return (from c in _context.Campaigns orderby c.CreateDate descending select c).ToList();
            }
        }
    }
}