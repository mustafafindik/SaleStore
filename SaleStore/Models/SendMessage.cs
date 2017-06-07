using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class SendMessage:BaseEntity
    {

        [Required]
        [StringLength(200)]
        [Display(Name = "Mailin Konusu")]
        public string Subject { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Mailin İçeriği")]
        public string BodyContent { get; set; }
        [Display(Name="Mail Ayarı")]
        public int MailSettingId { get; set; }
        [ForeignKey("MailSettingId")]
        public MailSetting MailSetting { get; set; }

    }
}
