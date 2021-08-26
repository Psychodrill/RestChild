namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecurityLogBugFixMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountHistoryLogin", "AccountId", "dbo.Account");
            DropIndex("dbo.AccountHistoryLogin", new[] { "EidSendStatus" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "Eid" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "AccountId" });
            DropTable("dbo.AccountHistoryLogin");


            CreateTable(
               "dbo.AccountHistoryLogin",
               c => new
                  {
                     Id = c.Long(nullable: false, identity: true),
                     DateEnter = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                     DateLastActivity = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                     DateExit = c.DateTime(precision: 7, storeType: "datetime2"),
                     SessionUid = c.String(maxLength: 1000),
                     RemoteAddr = c.String(maxLength: 1000),
                     UserAgent = c.String(),
                     StopSession = c.Boolean(nullable: false),
                     Login = c.String(maxLength: 1000),
                     IsAuthorized = c.Boolean(nullable: false),
                     AccountId = c.Long(),
                     LastUpdateTick = c.Long(nullable: false),
                     Eid = c.Long(),
                     EidSendStatus = c.Long(),
                     EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                  })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Account", t => t.AccountId)
               .Index(t => t.AccountId)
               .Index(t => t.Eid)
               .Index(t => t.EidSendStatus);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountHistoryLogin", "AccountId", "dbo.Account");
            DropIndex("dbo.AccountHistoryLogin", new[] { "EidSendStatus" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "Eid" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "AccountId" });
            DropTable("dbo.AccountHistoryLogin");


            CreateTable(
               "dbo.AccountHistoryLogin",
               c => new
                  {
                     Id = c.Long(nullable: false),
                     DateEnter = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                     DateLastActivity = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                     DateExit = c.DateTime(precision: 7, storeType: "datetime2"),
                     SessionUid = c.String(maxLength: 1000),
                     RemoteAddr = c.String(maxLength: 1000),
                     UserAgent = c.String(),
                     StopSession = c.Boolean(nullable: false),
                     Login = c.String(maxLength: 1000),
                     IsAuthorized = c.Boolean(nullable: false),
                     AccountId = c.Long(),
                     LastUpdateTick = c.Long(nullable: false),
                     Eid = c.Long(),
                     EidSendStatus = c.Long(),
                     EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                  })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Account", t => t.AccountId)
               .Index(t => t.AccountId)
               .Index(t => t.Eid)
               .Index(t => t.EidSendStatus);
        }
    }
}
