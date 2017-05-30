using SaleStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Product:BaseEntity
    {
        [DisplayName("Ürün Adı"),
         Required(ErrorMessage = "Lütfen bir {0} giriniz."),
         MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(35, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Ürün Detayı"),
         MaxLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Details { get; set; }

        [DisplayName("Birim Fiyat"),Required(ErrorMessage = "Lütfen bir {0} değeri giriniz."),
            DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayName("İndirimli Fiyat"),
            Required(ErrorMessage = "Lütfen bir {0} değeri giriniz."),
             DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }

        [DisplayName("İndirim Başlangıç Tarihi"),
             DataType(DataType.DateTime)]
        public DateTime SaleStarthDate { get; set; }

        [DisplayName("İndirim Bitiş Tarihi"),
             DataType(DataType.DateTime)]
        public DateTime SaleEndDate { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [DisplayName("Ürün Resmi")]
        public string ProductImage { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        
    }
}
