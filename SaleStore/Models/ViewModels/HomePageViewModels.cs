using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models.ViewModels
{
    public class HomePageViewModels 
    {
        
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Campaign> Campaigns { get; set; }
    }
}
