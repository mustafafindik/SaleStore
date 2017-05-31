using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class BaseEntity
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "Oluşturma Tarihi Girilmemiş")] 
        public DateTime CreateDate { get; set; }

        [StringLength(30,ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.")]
        [Display(Name = "Oluşturan")]
        public string CreatedBy { get; set; }

        [StringLength(30, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.")]
        [Display(Name = "Güncelleyen")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "Güncelleme Tarihi Girilmemiş")]
        public DateTime UpdateDate { get; set; }
       

    }
}
