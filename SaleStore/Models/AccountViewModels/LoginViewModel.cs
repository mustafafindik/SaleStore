using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage ="{0} alanı boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
