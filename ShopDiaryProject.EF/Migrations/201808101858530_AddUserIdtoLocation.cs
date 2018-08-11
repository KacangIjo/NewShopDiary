namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdtoLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Locations", new[] { "Location_Id" });
            RenameColumn(table: "dbo.Locations", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Locations", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            AddColumn("dbo.Locations", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.Locations", "Location_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Location_Id", c => c.Guid());
            DropColumn("dbo.Locations", "UserId");
            RenameIndex(table: "dbo.Locations", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Locations", name: "User_Id", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Locations", "Location_Id");
            AddForeignKey("dbo.Locations", "Location_Id", "dbo.Locations", "Id");
        }
    }
}
