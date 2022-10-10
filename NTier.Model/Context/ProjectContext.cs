using NTier.Core.Entity;
using NTier.Model.Entities;
using NTier.Model.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=.;Database=NTierProjectDb;Trusted_Connection=True;";
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Konfigürasyonlara hazırladığımız map sınıflarını ekliyoruz.
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new OrderDetailsMap());
            modelBuilder.Configurations.Add(new OrdersMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<SubCategory> SubCategories{ get; set; }
        public DbSet<OrderDetails> OrderDetails{ get; set; }
        public DbSet<Orders> Orders { get; set; }

        //SaveChanges içerisinde ekleme ve güncelleme bilgilerinin otomatik girişini sağlıyoruz.

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            string identity = WindowsIdentity.GetCurrent().Name;
            string computerName = Environment.MachineName;
            DateTime dateTime = DateTime.Now;
            int user = 1;
            string ip = "1";

            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                if (item != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedAtUserName = identity;
                        entity.CreatedComputerName = computerName;
                        entity.CreatedDate = dateTime;
                        entity.CreatedBy = user;
                        entity.CreatedIp = ip;
                    }
                    else if(item.State == EntityState.Modified)
                    {
                        entity.ModifiedAtUserName = identity;
                        entity.CreatedComputerName = computerName;
                        entity.ModifiedBy = user;
                        entity.ModifiedIp = ip;
                        entity.ModifiedDate = dateTime;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
