namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExchangeUtsAccountMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CertificateToApply",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CertificateKey = c.String(maxLength: 1000),
                        ByDefault = c.Boolean(nullable: false),
                        NotificationType = c.String(maxLength: 1000),
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
            
            CreateTable(
                "dbo.CertificateToApplyAccount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ForExcept = c.Boolean(nullable: false),
                        ForSystemAccount = c.Boolean(nullable: false),
                        AccountId = c.Long(),
                        CertificateToApplyId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.CertificateToApply", t => t.CertificateToApplyId)
                .Index(t => t.AccountId)
                .Index(t => t.CertificateToApplyId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.ExchangeUTS", "AccountId", c => c.Long());
            CreateIndex("dbo.ExchangeUTS", "AccountId");
            AddForeignKey("dbo.ExchangeUTS", "AccountId", "dbo.Account", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExchangeUTS", "AccountId", "dbo.Account");
            DropForeignKey("dbo.CertificateToApplyAccount", "CertificateToApplyId", "dbo.CertificateToApply");
            DropForeignKey("dbo.CertificateToApplyAccount", "AccountId", "dbo.Account");
            DropForeignKey("dbo.CertificateToApply", "AccountId", "dbo.Account");
            DropIndex("dbo.ExchangeUTS", new[] { "AccountId" });
            DropIndex("dbo.CertificateToApplyAccount", new[] { "EidSendStatus" });
            DropIndex("dbo.CertificateToApplyAccount", new[] { "Eid" });
            DropIndex("dbo.CertificateToApplyAccount", new[] { "CertificateToApplyId" });
            DropIndex("dbo.CertificateToApplyAccount", new[] { "AccountId" });
            DropIndex("dbo.CertificateToApply", new[] { "EidSendStatus" });
            DropIndex("dbo.CertificateToApply", new[] { "Eid" });
            DropIndex("dbo.CertificateToApply", new[] { "AccountId" });
            DropColumn("dbo.ExchangeUTS", "AccountId");
            DropTable("dbo.CertificateToApplyAccount");
            DropTable("dbo.CertificateToApply");
        }
    }
}
