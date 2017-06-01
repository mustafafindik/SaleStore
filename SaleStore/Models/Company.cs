using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Company:BaseEntity
    {
        [DisplayName("Firma Adı"),
         Required(ErrorMessage = "Lütfen bir {0} giriniz."),
         MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(35, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Adres"),
         MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(80, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Address { get; set;}
        [DisplayName("Telefon"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DisplayName("Firma Logosu")]
        public string Logo { get; set; }


    }
}
