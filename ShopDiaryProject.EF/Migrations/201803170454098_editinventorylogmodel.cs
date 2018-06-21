namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editinventorylogmodel : DbMigration
    {
        public override void Up()
        {
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
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventorylogs", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Inventorylogs", new[] { "InventoryId" });
            DropTable("dbo.Inventorylogs");
        }
    }
}
