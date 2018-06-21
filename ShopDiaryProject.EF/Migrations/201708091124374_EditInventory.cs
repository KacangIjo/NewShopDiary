namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditInventory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consumes", "IsConsumed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Inventories", "ItemName", c => c.String());
            AddColumn("dbo.Inventories", "IsConsumed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "IsConsumed");
            DropColumn("dbo.Inventories", "ItemName");
            DropColumn("dbo.Consumes", "IsConsumed");
        }
    }
}
