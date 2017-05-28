using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models.ViewModels
{
    public class HomePageViewModels
    {
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
