namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testBoom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wishlists", "ProductID", "dbo.Products");
            DropIndex("dbo.Wishlists", new[] { "ProductID" });
            DropTable("dbo.Wishlists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Wishlists", "ProductID");
            AddForeignKey("dbo.Wishlists", "ProductID", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
