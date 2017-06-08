using Microsoft.EntityFrameworkCore;
using SaleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStore.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ApplicationDbContext context)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();
            
            // Perform seed operations
            AddCategories(context);
            AddSettings(context);
        }
        public static void AddCategories(ApplicationDbContext context)
        {
            context.AddRange(
                new Category { Name = "Elektronik", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Moda-Takı", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Ev,Yaşam,Kırtasiye,Ofis", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Spor-Outdoor", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Kozmetik", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Kitap,Müzik,Film,Hobi", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Bebek Ürünleri,Oyuncaklar", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Ayakkabı-Çanta", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Gıda,Market", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now },
                new Category { Name = "Aksesuar", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now });
            context.SaveChanges();
        }
        private static void AddSettings(ApplicationDbContext context)
        {
            var s = new Setting();
            s.Logo = "";
            s.Phone = "+90 506 111 22 33";
            s.Address = "Bahariye Caddesi Süleymanpaşa Sokak No:2";
            s.Mail = "yardim@salestore.com";
            s.SiteSlogan = "Herşeyi indirdik sizi bekliyoruz";
            s.About = "EN kaliteli ürünleri EN uygun fiyatlarla bulmanın en kolay yolu";
            s.Title = "EN kaliteli ürünleri EN uygun fiyatlarla bulmanın en kolay yolu";
            s.SeoTitle = "EN kaliteli ürünleri EN uygun fiyatlarla bulmanın en kolay yolu";
            s.SeoDescription = "EN kaliteli ürünleri EN uygun fiyatlarla bulmanın en kolay yolu";
            s.SeoKeywords = "shop,alışveriş,ucuzu,indirim,en uygun fiyat,ürün,eşya,beyaz eşya,elektronik,ev aletleri,bebek ürünleri";
            s.Facebook = "";
            s.Twitter = "";
            s.LinkedIn = "";
            s.CreateDate = DateTime.Now;
            s.CreatedBy = "username";
            s.UpdateDate = DateTime.Now;
            s.UpdatedBy = "username";
            context.Setting.Add(s);
            context.SaveChanges();
        }
    }
}
