using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Advertisement : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(Models.AdvertisementType))]
        public AdvertisementType advType { get; set; }

        public bool HomePageCampaigns { get; set; }
        public bool HomePageProducts { get; set; }
        public bool ProductsPage { get; set; }
        public bool CampaignPage { get; set; }

        public string HTMLCodes { get; set; }
        public string URLpath{ get; set; }
        public string Image { get; set; }
        public bool Ispublished { get; set; }
        public DateTime AdvertiseStartDate { get; set; }
        public DateTime AdvertiseEndDate { get; set; }
        public double ClickRate { get; set; }
        public int PositionDegree { get; set; }


    }

}
