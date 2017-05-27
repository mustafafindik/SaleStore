using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Campaign:BaseEntity
    {
        public string Name { get; set; }
        public string CatchWord { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string Resim { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
