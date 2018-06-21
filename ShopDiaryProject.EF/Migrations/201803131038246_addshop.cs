namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addshop : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
                "dbo.Shopitems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        ShoplistId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
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
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Categories", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Categories", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Categories", "User_Id");
            AddForeignKey("dbo.Categories", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Consumes", "Quantity");
            DropColumn("dbo.Consumes", "Price");
            DropColumn("dbo.Storages", "Block");
          
        }
        
        public override void Down()
        {
            
            
            AddColumn("dbo.Storages", "Block", c => c.String(maxLength: 200));
            AddColumn("dbo.Consumes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Consumes", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Shopitems", "ShoplistId", "dbo.Shoplists");
            DropForeignKey("dbo.Shopitems", "ProductId", "dbo.Products");
            DropIndex("dbo.Shopitems", new[] { "ShoplistId" });
            DropIndex("dbo.Shopitems", new[] { "ProductId" });
            DropIndex("dbo.Categories", new[] { "User_Id" });
            DropColumn("dbo.Categories", "User_Id");
            DropColumn("dbo.Categories", "UserId");
            DropTable("dbo.Shoplists");
            DropTable("dbo.Shopitems");
          
        }
    }
}
