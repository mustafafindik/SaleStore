using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Campaign:BaseEntity
    {
        [DisplayName("Kampanya Adı"),
         Required(ErrorMessage = "Lütfen bir {0} giriniz."),
         MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(35, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Slogan { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [DisplayName("Kampanya Başlangıç Tarihi"),
             DataType(DataType.DateTime)]
        public DateTime CampaignStartDate { get; set; }

        [DisplayName("Kampanya Başlangıç Tarihi"),
             DataType(DataType.DateTime)]
        public DateTime CampaignEndDate { get; set; }
        [DisplayName("Kampanya Resmi")]
        public string Image { get; set; }

        

    }
}
