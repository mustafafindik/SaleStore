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

        public long Id { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }

        [StringLength(200)]
        [Display(Name = "Oluşturan")]
        public string CreatedBy { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime UpdateDate { get; set; }
       

    }
}
