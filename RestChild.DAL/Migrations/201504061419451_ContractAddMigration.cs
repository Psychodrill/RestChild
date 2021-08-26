namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractAddMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SignNumber = c.String(nullable: false, maxLength: 1000),
                        SignDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Summa = c.Decimal(nullable: false, precision: 32, scale: 4),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ContractDescription = c.String(),
                        YearOfRestId = c.Long(),
                        HistoryLinkId = c.Long(),
                        OrganizationId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.OrganizationId)
                .Index(t => t.StateId);
            
            AddColumn("dbo.Tour", "ContractId", c => c.Long());
            CreateIndex("dbo.Tour", "ContractId");
            AddForeignKey("dbo.Tour", "ContractId", "dbo.Contract", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "ContractId", "dbo.Contract");
            DropForeignKey("dbo.Contract", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.Contract", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Contract", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Contract", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.Contract", new[] { "StateId" });
            DropIndex("dbo.Contract", new[] { "OrganizationId" });
            DropIndex("dbo.Contract", new[] { "HistoryLinkId" });
            DropIndex("dbo.Contract", new[] { "YearOfRestId" });
            DropIndex("dbo.Tour", new[] { "ContractId" });
            DropColumn("dbo.Tour", "ContractId");
            DropTable("dbo.Contract");
        }
    }
}
