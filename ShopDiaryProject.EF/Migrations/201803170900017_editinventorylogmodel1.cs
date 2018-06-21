namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editinventorylogmodel1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Inventories", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "Quantity", c => c.Int(nullable: false));
        }
    }
}
