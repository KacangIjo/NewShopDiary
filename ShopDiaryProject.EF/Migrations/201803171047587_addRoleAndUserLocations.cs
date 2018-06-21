namespace ShopDiaryProject.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRoleAndUserLocations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Locations", new[] { "User_Id" });
            CreateTable(
                "dbo.UserLocations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 250),
                        RoleLocationId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        LocationId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.RoleLocations", t => t.RoleLocationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.RoleLocationId)
                .Index(t => t.LocationId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RoleLocations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleCode = c.Int(nullable: false),
                        Description = c.String(maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        CreatedUserId = c.String(),
                        ModifiedUserId = c.String(),
                        DeletedUserID = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Locations", "UserID");
            DropColumn("dbo.Locations", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Locations", "UserID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.UserLocations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserLocations", "RoleLocationId", "dbo.RoleLocations");
            DropForeignKey("dbo.UserLocations", "LocationId", "dbo.Locations");
            DropIndex("dbo.UserLocations", new[] { "User_Id" });
            DropIndex("dbo.UserLocations", new[] { "LocationId" });
            DropIndex("dbo.UserLocations", new[] { "RoleLocationId" });
            DropTable("dbo.RoleLocations");
            DropTable("dbo.UserLocations");
            CreateIndex("dbo.Locations", "User_Id");
            AddForeignKey("dbo.Locations", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
