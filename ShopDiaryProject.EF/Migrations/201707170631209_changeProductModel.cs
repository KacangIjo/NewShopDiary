namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProductModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inventories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Products", new[] { "InventoryId" });
            DropIndex("dbo.Inventories", new[] { "Product_Id" });
            DropColumn("dbo.Products", "InventoryId");
            DropColumn("dbo.Inventories", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "Product_Id", c => c.Guid());
            AddColumn("dbo.Products", "InventoryId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Inventories", "Product_Id");
            CreateIndex("dbo.Products", "InventoryId");
            AddForeignKey("dbo.Products", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Inventories", "Product_Id", "dbo.Products", "Id");
        }
    }
}
