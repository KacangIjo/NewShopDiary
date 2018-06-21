namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddedUserIdFullAuditedEnntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "AddedUserId", c => c.String());
            AddColumn("dbo.Products", "AddedUserId", c => c.String());
            AddColumn("dbo.Shopitems", "AddedUserId", c => c.String());
            AddColumn("dbo.Shoplists", "AddedUserId", c => c.String());
            AddColumn("dbo.UserLocations", "AddedUserId", c => c.String());
            AddColumn("dbo.Locations", "AddedUserId", c => c.String());
            AddColumn("dbo.Storages", "AddedUserId", c => c.String());
            AddColumn("dbo.Inventories", "AddedUserId", c => c.String());
            AddColumn("dbo.Inventorylogs", "AddedUserId", c => c.String());
            AddColumn("dbo.RoleLocations", "AddedUserId", c => c.String());
            AddColumn("dbo.Consumes", "AddedUserId", c => c.String());
            AddColumn("dbo.Purchases", "AddedUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "AddedUserId");
            DropColumn("dbo.Consumes", "AddedUserId");
            DropColumn("dbo.RoleLocations", "AddedUserId");
            DropColumn("dbo.Inventorylogs", "AddedUserId");
            DropColumn("dbo.Inventories", "AddedUserId");
            DropColumn("dbo.Storages", "AddedUserId");
            DropColumn("dbo.Locations", "AddedUserId");
            DropColumn("dbo.UserLocations", "AddedUserId");
            DropColumn("dbo.Shoplists", "AddedUserId");
            DropColumn("dbo.Shopitems", "AddedUserId");
            DropColumn("dbo.Products", "AddedUserId");
            DropColumn("dbo.Categories", "AddedUserId");
        }
    }
}
