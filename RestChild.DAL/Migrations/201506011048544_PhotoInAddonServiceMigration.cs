namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoInAddonServiceMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddonServicesPhoto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileTitle = c.String(nullable: false, maxLength: 1000),
                        DataCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AccountId = c.Long(),
                        AddonServicesId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .Index(t => t.AccountId)
                .Index(t => t.AddonServicesId);
            
            AddColumn("dbo.AddonServices", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.AddonServices", "AccountId", c => c.Long());
            CreateIndex("dbo.AddonServices", "HistoryLinkId");
            CreateIndex("dbo.AddonServices", "AccountId");
            AddForeignKey("dbo.AddonServices", "AccountId", "dbo.Account", "Id");
            AddForeignKey("dbo.AddonServices", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddonServicesPhoto", "AddonServicesId", "dbo.AddonServices");
            DropForeignKey("dbo.AddonServicesPhoto", "AccountId", "dbo.Account");
            DropForeignKey("dbo.AddonServices", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.AddonServices", "AccountId", "dbo.Account");
            DropIndex("dbo.AddonServicesPhoto", new[] { "AddonServicesId" });
            DropIndex("dbo.AddonServicesPhoto", new[] { "AccountId" });
            DropIndex("dbo.AddonServices", new[] { "AccountId" });
            DropIndex("dbo.AddonServices", new[] { "HistoryLinkId" });
            DropColumn("dbo.AddonServices", "AccountId");
            DropColumn("dbo.AddonServices", "HistoryLinkId");
            DropTable("dbo.AddonServicesPhoto");
        }
    }
}
