namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForCalculationFlagMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        HistoryLinkId = c.Long(),
                        HotelsId = c.Long(),
                        StateId = c.Long(),
                        LinkToFileId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Hotels", t => t.HotelsId)
                .ForeignKey("dbo.LinkToFile", t => t.LinkToFileId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.HotelsId)
                .Index(t => t.StateId)
                .Index(t => t.LinkToFileId);
            
            AddColumn("dbo.TypeOfRest", "NeedTransport", c => c.Boolean());
            AddColumn("dbo.TypeOfRest", "NeedRecordingDate", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "HaveMainService", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "TypeOfServiceId", c => c.Long());
            AddColumn("dbo.Tour", "MultiEventGeography", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "CountRequestLimited", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "ProductId", c => c.Long());
            AddColumn("dbo.EventGeography", "ProductId", c => c.Long());
            AddColumn("dbo.EventGeography", "ParentId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "ForCalculation", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestAccommodation", "ForCalculation", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestAccommodationLink", "ForCalculation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ticket", "ForCalculation", c => c.Boolean(nullable: false));
            AddColumn("dbo.TicketLink", "ForCalculation", c => c.Boolean(nullable: false));
            CreateIndex("dbo.TypeOfRest", "TypeOfServiceId");
            CreateIndex("dbo.Tour", "ProductId");
            CreateIndex("dbo.EventGeography", "ProductId");
            CreateIndex("dbo.EventGeography", "ParentId");
            AddForeignKey("dbo.TypeOfRest", "TypeOfServiceId", "dbo.TypeOfService", "Id");
            AddForeignKey("dbo.EventGeography", "ParentId", "dbo.EventGeography", "Id");
            AddForeignKey("dbo.EventGeography", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Tour", "ProductId", "dbo.Product", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "ProductId", "dbo.Product");
            DropForeignKey("dbo.EventGeography", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.Product", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.Product", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.Product", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.EventGeography", "ParentId", "dbo.EventGeography");
            DropForeignKey("dbo.TypeOfRest", "TypeOfServiceId", "dbo.TypeOfService");
            DropIndex("dbo.Product", new[] { "LinkToFileId" });
            DropIndex("dbo.Product", new[] { "StateId" });
            DropIndex("dbo.Product", new[] { "HotelsId" });
            DropIndex("dbo.Product", new[] { "HistoryLinkId" });
            DropIndex("dbo.EventGeography", new[] { "ParentId" });
            DropIndex("dbo.EventGeography", new[] { "ProductId" });
            DropIndex("dbo.Tour", new[] { "ProductId" });
            DropIndex("dbo.TypeOfRest", new[] { "TypeOfServiceId" });
            DropColumn("dbo.TicketLink", "ForCalculation");
            DropColumn("dbo.Ticket", "ForCalculation");
            DropColumn("dbo.RequestAccommodationLink", "ForCalculation");
            DropColumn("dbo.RequestAccommodation", "ForCalculation");
            DropColumn("dbo.AddonServicesLink", "ForCalculation");
            DropColumn("dbo.EventGeography", "ParentId");
            DropColumn("dbo.EventGeography", "ProductId");
            DropColumn("dbo.Tour", "ProductId");
            DropColumn("dbo.Tour", "CountRequestLimited");
            DropColumn("dbo.Tour", "MultiEventGeography");
            DropColumn("dbo.TypeOfRest", "TypeOfServiceId");
            DropColumn("dbo.TypeOfRest", "HaveMainService");
            DropColumn("dbo.TypeOfRest", "NeedRecordingDate");
            DropColumn("dbo.TypeOfRest", "NeedTransport");
            DropTable("dbo.Product");
        }
    }
}
