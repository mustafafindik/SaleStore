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
        public string Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string Productİmage { get; set; }
        public string CompanyName { get; set; }


    }
}
