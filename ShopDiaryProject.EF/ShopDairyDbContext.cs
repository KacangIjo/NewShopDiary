using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;

namespace ShopDiaryProject.EF
{
    public class ShopDiaryDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopDiaryDbContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
          
        }

        public static ShopDiaryDbContext Create()
        {
            return new ShopDiaryDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is FullAuditedEntity
                    && (x.State == System.Data.Entity.EntityState.Added
                     || x.State == System.Data.Entity.EntityState.Modified
                     || x.State == System.Data.Entity.EntityState.Deleted));

            foreach (var entry in modifiedEntries)
            {
                FullAuditedEntity entity = entry.Entity as FullAuditedEntity;
                if (entity != null && Thread.CurrentPrincipal.Identity != null)
                {
                    string identityId = Thread.CurrentPrincipal.Identity.GetUserId();
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedUserId = identityId;
                        entity.CreatedDate = now;
                    }
                    else if (entry.State == System.Data.Entity.EntityState.Deleted)
                    {
                        entity.DeletedUserID = identityId;
                        entity.DeletedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedUserId).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.ModifiedUserId = identityId;
                    entity.ModifiedDate = now;
                }
            }
            return base.SaveChanges();
        }

       
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Inventorylog> Inventorylogs { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Consume> Consumes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Shopitem> Shopitems { get; set; }
        public virtual DbSet<Shoplist> Shoplists { get; set; }
        public virtual DbSet<UserLocation> UserLocations { get; set; }
        public virtual DbSet<UserData> UserDatas { get; set; }
     

    }
    

}