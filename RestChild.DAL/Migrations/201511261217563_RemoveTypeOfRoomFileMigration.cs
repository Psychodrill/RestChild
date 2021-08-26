namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTypeOfRoomFileMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TypeOfRoomsFiles", new[] { "TypeOfRoomsId" });
            CreateTable(
                "dbo.SubjectOfRestClassification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        ViewOnSite = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        HistoryLinkId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .Index(t => t.HistoryLinkId);
            
            AddColumn("dbo.SubjectOfRest", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.SubjectOfRest", "SubjectOfRestClassificationId", c => c.Long());
            CreateIndex("dbo.SubjectOfRest", "HistoryLinkId");
            CreateIndex("dbo.SubjectOfRest", "SubjectOfRestClassificationId");
            AddForeignKey("dbo.SubjectOfRest", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.SubjectOfRest", "SubjectOfRestClassificationId", "dbo.SubjectOfRestClassification", "Id");
            DropTable("dbo.TypeOfRoomsFiles");
        }
        
        public override void Down()
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
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.SubjectOfRest", "SubjectOfRestClassificationId", "dbo.SubjectOfRestClassification");
            DropForeignKey("dbo.SubjectOfRestClassification", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.SubjectOfRest", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.SubjectOfRestClassification", new[] { "HistoryLinkId" });
            DropIndex("dbo.SubjectOfRest", new[] { "SubjectOfRestClassificationId" });
            DropIndex("dbo.SubjectOfRest", new[] { "HistoryLinkId" });
            DropColumn("dbo.SubjectOfRest", "SubjectOfRestClassificationId");
            DropColumn("dbo.SubjectOfRest", "HistoryLinkId");
            DropTable("dbo.SubjectOfRestClassification");
            CreateIndex("dbo.TypeOfRoomsFiles", "TypeOfRoomsId");
        }
    }
}
