using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SaleStore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductCount=25;
            CampaignCount = 3;
        }
        [DisplayName("Ad"),
         Required(ErrorMessage = "Lütfen bir {0} giriniz."),
         MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
         MaxLength(35, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Ürün Sayısı")]
        public int ProductCount { get; set; }
        [DisplayName("Kampanya Sayısı")]
        public int CampaignCount { get; set; }

        public string Avatar { get; set; }    
        
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
    }
}
