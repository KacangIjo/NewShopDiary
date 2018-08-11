namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        Description = c.String(maxLength: 200),
                        UserId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        BarcodeId = c.String(maxLength: 250),
                        CategoryId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Shopitems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        ShoplistId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Shoplists", t => t.ShoplistId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ShoplistId);
            
            CreateTable(
                "dbo.Shoplists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Market = c.String(maxLength: 200),
                        Description = c.String(maxLength: 300),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Address = c.String(maxLength: 150),
                        Description = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Location_Id = c.Guid(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Area = c.String(maxLength: 200),
                        LocationId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        ItemName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsConsumed = c.Boolean(nullable: false),
                        StorageId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Inventorylogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LogDate = c.DateTime(),
                        Description = c.String(maxLength: 300),
                        InventoryId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.UserLocations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegisteredUser = c.Guid(nullable: false),
                        Description = c.String(maxLength: 250),
                        RoleLocationId = c.Guid(nullable: false),
                        LocationId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.RoleLocations", t => t.RoleLocationId, cascadeDelete: true)
                .Index(t => t.RoleLocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.RoleLocations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleCode = c.Int(nullable: false),
                        Description = c.String(maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Consumes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateConsumed = c.DateTime(nullable: false),
                        IsConsumed = c.Boolean(nullable: false),
                        InventoryId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        Market = c.String(maxLength: 250),
                        InventoryId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        AddedUserId = c.String(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Purchases", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Consumes", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Locations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserLocations", "RoleLocationId", "dbo.RoleLocations");
            DropForeignKey("dbo.UserLocations", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Storages", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Inventories", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.Inventories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Inventorylogs", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Locations", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Shopitems", "ShoplistId", "dbo.Shoplists");
            DropForeignKey("dbo.Shopitems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Purchases", new[] { "InventoryId" });
            DropIndex("dbo.Consumes", new[] { "InventoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserLocations", new[] { "LocationId" });
            DropIndex("dbo.UserLocations", new[] { "RoleLocationId" });
            DropIndex("dbo.Inventorylogs", new[] { "InventoryId" });
            DropIndex("dbo.Inventories", new[] { "ProductId" });
            DropIndex("dbo.Inventories", new[] { "StorageId" });
            DropIndex("dbo.Storages", new[] { "LocationId" });
            DropIndex("dbo.Locations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Locations", new[] { "Location_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Shopitems", new[] { "ShoplistId" });
            DropIndex("dbo.Shopitems", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "User_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Purchases");
            DropTable("dbo.Consumes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.RoleLocations");
            DropTable("dbo.UserLocations");
            DropTable("dbo.Inventorylogs");
            DropTable("dbo.Inventories");
            DropTable("dbo.Storages");
            DropTable("dbo.Locations");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Shoplists");
            DropTable("dbo.Shopitems");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
