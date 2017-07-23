using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen bir {0} giriniz.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Lütfen geçerli formatta bir mail adresi giriniz.")]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}  {2} karakter ile {1} karakter arasında olmalıdır.", MinimumLength = 6)]       
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre(Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifre ve Şifre(Tekrar) eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Firma Adı"),
        Required(ErrorMessage = "Lütfen bir {0} giriniz."),
        MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
        MaxLength(35, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Adres"),
         MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(80, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Address { get; set; }
        [DisplayName("Telefon"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Üyelik Sözleşmesi ve Gizlilik Prensiplerini Onaylamadan Üye Olamazsınız")]
        [DisplayName("Üyelik Sözleşmesini ve Gizlilik Prensiplerini Kabul Ediyorum")]
        public bool CompanyAcceptAgreement { get; set; }

        [DisplayName("Firma Logosu")]
        public string Logo { get; set; }

        [DisplayName("Ürün Sayısı")]
        public int ProductCount { get; set; }
        [DisplayName("Kampanya Sayısı")]
        public int CampaignCount { get; set; }

    }
}
