using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Category:BaseEntity
    {
        public Category()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
           
        }
        public string Name { get; set; }
    }
}
