using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Bu tarayıcıda hatırlansın mı?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Beni hatırla.")]
        public bool RememberMe { get; set; }
    }
}
