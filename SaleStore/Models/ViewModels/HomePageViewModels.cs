using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models.ViewModels
{
    public class HomePageViewModels 
    {
        
        public IPagedList<Category> Categories { get; set; }
        public IPagedList<Product> Products { get; set; }
        public IPagedList<Campaign> Campaigns { get; set; }
    }
}
