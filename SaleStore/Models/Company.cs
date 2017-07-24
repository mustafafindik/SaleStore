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
        public Company()
        {

            ProductCount = 25;
            CampaignCount = 3;

        }

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

        [DisplayName("Firma Açıklama")]
        public string Description { get; set; }

        [DisplayName("Ürün Sayısı")]
        public int ProductCount { get; set; }
        [DisplayName("Kampanya Sayısı")]
        public int CampaignCount { get; set; }

        [DisplayName("Harita Gösterilsin mi")]
        public bool MapIsVisible { get; set; }

        [DisplayName("Üyelik Sözleşmesini ve Gizlilik Prensiplerini Kabul Ediyorum")]
        public bool CompanyAcceptAgreement { get; set; }

        [DisplayName("Harita Başlığı")]
        public string MapTitle { get; set; }

        [DisplayName("Enlem")]
        public string MapLat { get; set; }

        [DisplayName("Boylam")]
        public string MapLon { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
