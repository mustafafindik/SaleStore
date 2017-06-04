using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Setting : BaseEntity
    {
        public int Id { get; set; }

        [DisplayName("Logo"),Required(ErrorMessage = "Lütfen bir {0} giriniz.")]
        public string Logo { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        [Display(Name = "Site Sloganı")]
        public string SiteSlogan { get; set; }
        [Display(Name = "Hakkımızda")]
        public string About { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "SEO Başlık")]
        public string SeoTitle { get; set; }
        [Display(Name = "SEO Açıklama")]
        public string SeoDescription { get; set; }
        [Display(Name = "Anahtar Kelimeler")]
        public string SeoKeywords { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }


    }
}
