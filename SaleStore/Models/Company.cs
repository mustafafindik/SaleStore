using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set;}
        public string Phone { get; set; }
        public int CategoryId { get; set; }
        public string Logo { get; set; }
        public int UserId { get; set; }
    }
}
