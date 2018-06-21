namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inventories", "ProductId", "dbo.Products");
            AddColumn("dbo.Products", "InventoryId", c => c.Guid(nullable: false));
            AddColumn("dbo.Inventories", "Product_Id", c => c.Guid());
            CreateIndex("dbo.Products", "InventoryId");
            CreateIndex("dbo.Inventories", "Product_Id");
            AddForeignKey("dbo.Products", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Inventories", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Inventories", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "InventoryId" });
            DropColumn("dbo.Inventories", "Product_Id");
            DropColumn("dbo.Products", "InventoryId");
            AddForeignKey("dbo.Inventories", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
