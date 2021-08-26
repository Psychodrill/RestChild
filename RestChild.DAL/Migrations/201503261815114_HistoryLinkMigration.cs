namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryLinkMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryLink",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.History",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateChange = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EventCode = c.String(nullable: false, maxLength: 1000),
                        Commentary = c.String(),
                        LinkId = c.Long(),
                        AccountId = c.Long(),
                        SignInfoId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.HistoryLink", t => t.LinkId)
                .ForeignKey("dbo.SignInfo", t => t.SignInfoId)
                .Index(t => t.LinkId)
                .Index(t => t.AccountId)
                .Index(t => t.SignInfoId);
            
            AddColumn("dbo.LimitOnOrganization", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.ListOfChilds", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.LimitOnOrganization", "HistoryLinkId");
            CreateIndex("dbo.LimitOnVedomstvo", "HistoryLinkId");
            CreateIndex("dbo.ListOfChilds", "HistoryLinkId");
            AddForeignKey("dbo.LimitOnOrganization", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.LimitOnVedomstvo", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.ListOfChilds", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListOfChilds", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.LimitOnVedomstvo", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.LimitOnOrganization", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.History", "SignInfoId", "dbo.SignInfo");
            DropForeignKey("dbo.History", "LinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.History", "AccountId", "dbo.Account");
            DropIndex("dbo.ListOfChilds", new[] { "HistoryLinkId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "HistoryLinkId" });
            DropIndex("dbo.History", new[] { "SignInfoId" });
            DropIndex("dbo.History", new[] { "AccountId" });
            DropIndex("dbo.History", new[] { "LinkId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "HistoryLinkId" });
            DropColumn("dbo.ListOfChilds", "HistoryLinkId");
            DropColumn("dbo.LimitOnVedomstvo", "HistoryLinkId");
            DropColumn("dbo.LimitOnOrganization", "HistoryLinkId");
            DropTable("dbo.History");
            DropTable("dbo.HistoryLink");
        }
    }
}
