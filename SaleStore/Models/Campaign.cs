using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Campaign:BaseEntity
    {
        public string Name { get; set; }
        public string CatchWord { get; set; }
        public int CategoryId { get; set; }
        public string Resim { get; set; }

    }
}
