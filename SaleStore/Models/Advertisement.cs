using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Models
{
    public class Advertisement : BaseEntity
    {
        [DisplayName("Reklam Adı"),
          Required(ErrorMessage = "Lütfen bir {0} giriniz."),
          MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
          MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Reklam Başlığı"),
          
          MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır."),
          MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Reklam Detayı")]
        public string Description { get; set; }

        [DisplayName("Reklam Tipi")]
        [EnumDataType(typeof(Models.AdvertisementType))]
        public AdvertisementType advType { get; set; }

        [DisplayName("Ana Sayfada Kampanyalarda Gözüksün")]
        public bool HomePageCampaigns { get; set; }
        [DisplayName("Ana Sayfada Ürünlerde Gözüksün")]
        public bool HomePageProducts { get; set; }
        [DisplayName("Ürünler Sayfasında Gözüksün")]
        public bool ProductsPage { get; set; }
        [DisplayName("Kampanyalar Sayfasında Gözüksün")]
        public bool CampaignPage { get; set; }
        [DisplayName("HTML Kodu")]
        public string HTMLCodes { get; set; }
        [DisplayName("URL")]
        public string URLpath{ get; set; }
        [DisplayName("Resmi")]
        public string Image { get; set; }
        [DisplayName("Yayında Mı?")]
        public bool Ispublished { get; set; }
        [DisplayName("Reklam Başlangıç Tarih")]
        public DateTime AdvertiseStartDate { get; set; }
        [DisplayName("Reklam Bitiş Tarihi")]
        public DateTime AdvertiseEndDate { get; set; }
        [DisplayName("Tıklanma Sayısı")]
        public double ClickRate { get; set; }
        [DisplayName("Önem Derecesi")]
        public int PositionDegree { get; set; }


    }

}
