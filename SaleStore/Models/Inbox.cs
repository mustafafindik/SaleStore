using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Inbox
    {
        public Inbox()
        {

            SubmitDate = DateTime.Now;

        }


        public int Id { get; set; }
        [Display(Name = "Ad Soyad")]
        [Required]
        [StringLength(200)]
        public string FullName { get; set; }
        [Display(Name = "Telefon")]
        [Required]
        [StringLength(200)]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Display(Name = "Mesaj")]
        [Required]
        public string Message { get; set; }
        [Display(Name = "İletilme Tarihi"),
             DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime SubmitDate { get; set; }
        [Required]
        [StringLength(200)]
        public string Ip { get; set; }
    }
}
