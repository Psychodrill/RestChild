namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PedPartyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PedParty",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        City = c.String(maxLength: 1000),
                        DateCreate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                        HistoryLinkId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.StateId);
            
            AddColumn("dbo.Counselors", "PedPartyId", c => c.Long());
            CreateIndex("dbo.Counselors", "PedPartyId");
            AddForeignKey("dbo.Counselors", "PedPartyId", "dbo.PedParty", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counselors", "PedPartyId", "dbo.PedParty");
            DropForeignKey("dbo.PedParty", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.PedParty", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.PedParty", new[] { "StateId" });
            DropIndex("dbo.PedParty", new[] { "HistoryLinkId" });
            DropIndex("dbo.Counselors", new[] { "PedPartyId" });
            DropColumn("dbo.Counselors", "PedPartyId");
            DropTable("dbo.PedParty");
        }
    }
}
