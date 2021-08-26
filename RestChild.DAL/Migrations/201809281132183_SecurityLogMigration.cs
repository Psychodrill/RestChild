namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecurityLogMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "DateDelete", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Account", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "IsTemporyPassword", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account", "DateLastChangePassword", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Account", "CountUnsuccess", c => c.Int());
            AddColumn("dbo.Account", "DateLastUnsuccess", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Account", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.Account", "HistoryLinkId");
            AddForeignKey("dbo.Account", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.Account", new[] { "HistoryLinkId" });
            DropColumn("dbo.Account", "HistoryLinkId");
            DropColumn("dbo.Account", "DateLastUnsuccess");
            DropColumn("dbo.Account", "CountUnsuccess");
            DropColumn("dbo.Account", "DateLastChangePassword");
            DropColumn("dbo.Account", "IsTemporyPassword");
            DropColumn("dbo.Account", "IsDeleted");
            DropColumn("dbo.Account", "DateDelete");
        }
    }
}
