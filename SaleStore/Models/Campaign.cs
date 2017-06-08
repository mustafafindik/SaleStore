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
         MaxLength(250, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(500, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Slogan { get; set; }

        [DisplayName("Kampanya Açıklaması")]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [DisplayName("Kampanya Başlangıç Tarihi"),
           DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false,
           NullDisplayText = "Kampanya Başlangıç Tarihi Girilmemiş")]
        public DateTime CampaignStartDate { get; set; }

        [DisplayName("Kampanya Bitiş Tarihi"),
            DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false, 
            NullDisplayText = "Kampanya Bitiş Tarihi Girilmemiş")]
        public DateTime CampaignEndDate { get; set; }

        [DisplayName("Kampanya Resmi")]
        public string Image { get; set; }

        [DisplayName("Yayında mı?")]
        public bool IsPublish { get; set; }

    }
}
