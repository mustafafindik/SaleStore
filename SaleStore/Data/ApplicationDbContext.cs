using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaleStore.Models;
using SaleStore.Models.ViewModels;

namespace SaleStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
        public DbSet<Inbox> Inboxes { get; set; }
        public DbSet<SendMessage> SendMessages { get; set; }
        public DbSet<SaleStore.Models.Setting> Setting { get; set; }
        public DbSet<Role> ApplicationRoles { get; set; }
        public DbSet<SaleStore.Models.ApplicationUser> ApplicationUser { get; set; }
      
    }
}
