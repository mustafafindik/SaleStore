using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string CategoryId { get; set; }
        public string Productİmage { get; set; }


    }
}
