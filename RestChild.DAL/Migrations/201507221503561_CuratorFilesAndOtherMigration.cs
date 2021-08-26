namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CuratorFilesAndOtherMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfRoomsFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileLink = c.String(nullable: false, maxLength: 1000),
                        IsPhoto = c.Boolean(nullable: false),
                        IsVideo = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TypeOfRoomsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .Index(t => t.TypeOfRoomsId);
            
            AddColumn("dbo.AddonServices", "CuratorId", c => c.Long());
            AddColumn("dbo.Request", "CuratorId", c => c.Long());
            AddColumn("dbo.SubjectOfRest", "DescriptionHtml", c => c.String());
            AddColumn("dbo.SubjectOfRestFile", "IsPhoto", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubjectOfRestFile", "IsVideo", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AddonServices", "CuratorId");
            CreateIndex("dbo.Request", "CuratorId");
            AddForeignKey("dbo.AddonServices", "CuratorId", "dbo.Account", "Id");
            AddForeignKey("dbo.Request", "CuratorId", "dbo.Account", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "CuratorId", "dbo.Account");
            DropForeignKey("dbo.TypeOfRoomsFiles", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.AddonServices", "CuratorId", "dbo.Account");
            DropIndex("dbo.Request", new[] { "CuratorId" });
            DropIndex("dbo.TypeOfRoomsFiles", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.AddonServices", new[] { "CuratorId" });
            DropColumn("dbo.SubjectOfRestFile", "IsVideo");
            DropColumn("dbo.SubjectOfRestFile", "IsPhoto");
            DropColumn("dbo.SubjectOfRest", "DescriptionHtml");
            DropColumn("dbo.Request", "CuratorId");
            DropColumn("dbo.AddonServices", "CuratorId");
            DropTable("dbo.TypeOfRoomsFiles");
        }
    }
}
