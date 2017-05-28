using SaleStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SalePrice { get; set; }

        public DateTime SaleStarthDate { get; set; }
        public DateTime SaleEndDate { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string ProductImage { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public HomePageViewModels HomePageModels { get; set; }
    }
}
