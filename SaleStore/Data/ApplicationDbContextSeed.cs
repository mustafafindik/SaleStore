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
            
            // Perform seed operations
            AddCategories(context);
            AddSettings(context);
            AddCompanies(context);
            AddUsers(userManager);
            AddRoles(roleManager);
            AddRoleToUser(userManager);
        }


        private static ApplicationUser user;

        private static void AddUsers(UserManager<ApplicationUser> _userManager)
        {
            user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "salestoredeneme@gmail.com", Email = "salestoredeneme@gmail.com", EmailConfirmed = true, NormalizedEmail = "SALESTOREDENEME@GMAİL.COM", NormalizedUserName = "SALESTOREDENEME@GMAİL.COM", CompanyId = 1 };
            var task1 = Task.Run(() => _userManager.CreateAsync(user, "123:Asdf"));
            task1.Wait();

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
            s.Logo = "";
            s.Phone = "444 55 55";
            s.Address = "Bahariye Caddesi Süleymanpaşa Sokak No:2";
            s.Mail = "salestore@gmail.com";
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

        public static void AddCompanies(ApplicationDbContext context)
        {
            context.AddRange(
                new Company { Name = "OnKa Yazılım", Address = "Kadıköy", Phone = "01234567899", Logo = "/images/logo.png", CreatedBy = "username", CreateDate = DateTime.Now, UpdatedBy = "username", UpdateDate = DateTime.Now });
                
            context.SaveChanges();
        }
    }
}
