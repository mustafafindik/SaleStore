using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Kategori Adı"),
          Required(ErrorMessage = "Lütfen bir {0} giriniz."),
          MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
          MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
    }
}
