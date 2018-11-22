namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemNameToShopItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shopitems", "ItemName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shopitems", "ItemName");
        }
    }
}
