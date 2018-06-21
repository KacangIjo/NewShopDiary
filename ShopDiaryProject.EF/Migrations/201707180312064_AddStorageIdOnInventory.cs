namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStorageIdOnInventory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consumes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Consumes", "Price");
        }
    }
}
