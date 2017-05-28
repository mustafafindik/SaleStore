using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Campaign:BaseEntity
    {
        public string Name { get; set; }
        public string Slogan { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public DateTime CampaignStartDate { get; set; }
        public DateTime CampaignEndDate { get; set; }

        public string Image { get; set; }

        

    }
}
