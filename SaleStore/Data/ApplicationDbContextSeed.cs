using Microsoft.AspNetCore.Identity;
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
        public static void Seed(this ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();

            if (context.Setting.Any())
            {
                return;   // DB has been seeded
            }

            // Perform seed operations
            AddCompanies(context);
            AddCategories(context);
            AddCampaigns(context);
            AddProducts(context);
            AddSettings(context);
            AddMailSettings(context);
            AddSendMessages(context);

            //kullanıcı ve role ilişkisi
            AddUsers(userManager);
            AddRoles(roleManager);
            AddRoleToUser(userManager);
        }
        public static void AddProducts(ApplicationDbContext context)
        {
            context.AddRange(
                
                new Product
                {
                    Name = "iPhone 7",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId =1,
                    CategoryId = 1,
                    Details= "iPhone 7",
                    ProductImage= "uploads/Seed/iphone7.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice= 3509,
                    UnitPrice=4000,
                    IsPublish = true
                },
                new Product
                {
                    Name = "Emporio Armani",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 2,
                    Details = "Kadın Kol Saati AR1925",
                    ProductImage = "uploads/Seed/Saat_er.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 1099,
                    UnitPrice = 1999,
                    IsPublish = true
                },
                new Product
                {
                    Name = "Versatil Kalem",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 3,
                    Details = "Rotring 600 0.5 mm ",
                    ProductImage = "uploads/Seed/versatil-kalem.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 76,
                    UnitPrice = 90,
                    IsPublish = true
                },
                new Product
                {
                    Name = "KAMP ÇADIR",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 4,
                    Details = "4 MEVSİM ÜÇ KİŞİLİK KAMP ÇADIR TENTE SİNEKLİK",
                    ProductImage = "uploads/Seed/cadir.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 59,
                    UnitPrice = 89,
                    IsPublish = true
                },
                new Product
                {
                    Name = "Calvin Klein",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 5,
                    Details = "In2U Edt 50 ml Erkek Parfümü 088300196920",
                    ProductImage = "uploads/Seed/calvin.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 49,
                    UnitPrice = 150,
                    IsPublish = true
                },
                new Product
                {
                    Name = "Nutuk",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 6,
                    Details = "Kurtuluş savaşının öyküsü.",
                    ProductImage = "uploads/Seed/nutuk.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 5,
                    UnitPrice = 10,
                    IsPublish = true
                },
                new Product
                {
                    Name = "Cioccolata",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 7,
                    Details = "Beyaz Erkek Çocuk T-shirt 3232CIC1538",
                    ProductImage = "uploads/Seed/Cioccolata.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 11,
                    UnitPrice = 29,
                    IsPublish = true
                },
                
             
                new Product
                {
                    Name = "İncili Çatal İğne Sallantılı Küpe",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CompanyId = 1,
                    CategoryId = 10,
                    Details = "SS 17 % 45 BAKIR % 35 CAM % 15 KRİSTAL % 5 DEMİR",
                    ProductImage = "uploads/Seed/kupe.jpg",
                    SaleStarthDate = DateTime.Now,
                    SaleEndDate = DateTime.Now,
                    SalePrice = 15,
                    UnitPrice = 29,
                    IsPublish = true
                }



                   );

            context.SaveChanges();
        }
        public static void AddCampaigns(ApplicationDbContext context)
        {
            context.AddRange(

                new Campaign
                {
                    Name = "TEKNOSACELL ESKİ TELEFONUNU GETİR YENİSİNİ İNDİRİMLE GÖTÜR",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CampaignStartDate = DateTime.Now,
                    CampaignEndDate = DateTime.Now,
                    CategoryId = 1,
                    CompanyId = 1,
                    Slogan = "Telefonları yenileme zamanı",
                    Image = "uploads/Seed/eskiyeni.jpg",
                    Description = "İkinci el ürünler TECHPOİNT SERVİS HİZMETLERİ A.Ş. adına Teknosa tarafından teslim alınmakta olup, ikinci el cihaz bedeli karşılığında verilen hediye çeki ilgili firma tarafından karşılanmaktadır.",
                    IsPublish = true
                },
                
                new Campaign
                {
                    Name = "Seçili projeksiyon aletlerinde",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CampaignStartDate = DateTime.Now,
                    CampaignEndDate = DateTime.Now,
                    CategoryId = 3,
                    CompanyId = 1,
                    Slogan = "Sepete %15 indirim",
                    Image = "uploads/Seed/projeksiyon.png",
                    Description = "Satıcı hepsiburada olan ürünlerde geçerlidir.",
                    IsPublish = true
                },
               
                new Campaign
                {
                    Name = "Parfümde yaz fırsatı",
                    CreatedBy = "username",
                    CreateDate = DateTime.Now,
                    UpdatedBy = "username",
                    UpdateDate = DateTime.Now,
                    CampaignStartDate = DateTime.Now,
                    CampaignEndDate = DateTime.Now,
                    CategoryId = 5,
                    CompanyId = 1,
                    Slogan = "%80'e varan indirimler",
                    Image = "uploads/Seed/parfumyaz.jpg",
                    Description = "En iyi kadın ve erkek parfümlerinin yer aldığı dünyaca ünlü, Burberry, Chanel, Tom Ford, Bvlgari, Estee Lauder, Calvin Klein ve diğer markaların en çok satan ürünlerinden oluşan bu kampanya, aradığınız tüm parfümleri özel indirimlerle size sunuyor. Bu orijinal parfümlere, size özel indirim fırsatları ile sahip olma fırsatını kaçırmayın!",
                    IsPublish = true
                }
                
               
                   );
            context.SaveChanges();
        }


        private static ApplicationUser user;
        private static ApplicationUser user2;

        private static void AddUsers(UserManager<ApplicationUser> _userManager)
        {
            user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "salestoredeneme@gmail.com", Email = "salestoredeneme@gmail.com", EmailConfirmed = true, NormalizedEmail = "SALESTOREDENEME@GMAIL.COM", NormalizedUserName = "SALESTOREDENEME@GMAIL.COM"};
            var task1 = Task.Run(() => _userManager.CreateAsync(user, "123:Asdf"));
            task1.Wait();

            user2 = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "company@gmail.com", Email = "company@gmail.com", EmailConfirmed = true, NormalizedEmail = "COMPANY@GMAIL.COM", NormalizedUserName = "COMPANY@GMAIL.COM" };
            var task2 = Task.Run(() => _userManager.CreateAsync(user2, "123:Asdf"));
            task2.Wait();

        }

        private static void AddRoles(RoleManager<Role> _roleManager)
        {
            string[] roles = { "ADMIN" };
            string[] stamp = { "Yönetici" };

            for (int i = 0; i < roles.Count(); i++)
            {
                var role = new Role { Id = Guid.NewGuid().ToString(), Name = roles[i], NormalizedName = roles[i], ConcurrencyStamp = stamp[i] };
                var task1 = Task.Run(() => _roleManager.CreateAsync(role));
                task1.Wait();
            }
        }
        private static void AddRoleToUser(UserManager<ApplicationUser> _userManager)
        {
            var task1 = Task.Run(() => _userManager.AddToRoleAsync(user, "ADMIN"));
            task1.Wait();
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
            s.Logo = "/uploads/6-2017-Logos/logo-small.png";
            s.Phone = "+90 506 111 22 33";
            s.Address = "Bahariye Caddesi Süleymanpaşa Sokak No:2";
            s.Mail = "yardim@salestore.com";
            s.SiteSlogan = "Herşeyi indirdik sizi bekliyoruz";
            s.About = "EN kaliteli ürünleri EN uygun fiyatlarla bulmanın en kolay yolu";
            s.Vision = "Türkiye’de ve bölgede e-ticaret sektörünün lideri olmak.";
            s.Mission = "E-ticaret sektöründe hem müşterilere hem mağazalara yenilikçi hizmetler sunarak Türkiye e-ticaret sektörünün yeniden şekillendirilmesine öncülük etmek.";
            s.Strategy = "Stratejik ortaklıklarla güçlü entegrasyona dayanan eko-sistemimizde, müşterilere Güven ve Kolaylık; mağazalara ise Destek ve Özen üzerine dayalı değer önerileri sunmaktır.";
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

        public static void AddCompanies(ApplicationDbContext context)
        {
            context.AddRange(
                new Company { Name = "OnKa Yazılım", Address = "Kadıköy", Phone = "01234567899", Logo = "/uploads/logo.png", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now });
                
            context.SaveChanges();
        }
        public static void AddMailSettings(ApplicationDbContext context)
        {
            context.AddRange(
                new MailSetting { FromAddress="salestoredeneme@gmail.com", FromAddressPassword="123:Asdf",FromAddressTitle="Sale Store",SmptServer="smtp.gmail.com",SmptPortNumber=587, CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now });

            context.SaveChanges();
        }
        public static void AddSendMessages(ApplicationDbContext context)
        {
            context.AddRange(
                new SendMessage {  Subject="İletişim", BodyContent="Mesajınız bize iletilmiştir. Teşekür ederiz.", MailSettingId=1 ,CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now });

            context.SaveChanges();
        }
    }
}
