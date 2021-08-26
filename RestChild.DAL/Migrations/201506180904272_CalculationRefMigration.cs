namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalculationRefMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestRoomRates", "Request_Id", "dbo.Request");
            DropForeignKey("dbo.RequestRoomRates", "RoomRates_Id", "dbo.RoomRates");
            DropForeignKey("dbo.Child", "RoomRatesId", "dbo.RoomRates");
            DropForeignKey("dbo.Applicant", "RoomRatesId", "dbo.RoomRates");
            DropForeignKey("dbo.Calculation", "AddonServicesLinkId", "dbo.AddonServicesLink");
            DropForeignKey("dbo.Calculation", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.Calculation", "ChildId", "dbo.Child");
            DropForeignKey("dbo.Calculation", "TicketId", "dbo.Ticket");
            DropIndex("dbo.Applicant", new[] { "RoomRatesId" });
            DropIndex("dbo.Child", new[] { "RoomRatesId" });
            DropIndex("dbo.Calculation", new[] { "ChildId" });
            DropIndex("dbo.Calculation", new[] { "ApplicantId" });
            DropIndex("dbo.Calculation", new[] { "AddonServicesLinkId" });
            DropIndex("dbo.Calculation", new[] { "TicketId" });
            DropIndex("dbo.RequestRoomRates", new[] { "Request_Id" });
            DropIndex("dbo.RequestRoomRates", new[] { "RoomRates_Id" });
            CreateTable(
                "dbo.OfferInRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(),
                        RoomRatesId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.RoomRates", t => t.RoomRatesId)
                .Index(t => t.RequestId)
                .Index(t => t.RoomRatesId);
            
            CreateTable(
                "dbo.AddonServicesLinkCalculation",
                c => new
                    {
                        AddonServicesLink_Id = c.Long(nullable: false),
                        Calculation_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AddonServicesLink_Id, t.Calculation_Id })
                .ForeignKey("dbo.AddonServicesLink", t => t.AddonServicesLink_Id, cascadeDelete: true)
                .ForeignKey("dbo.Calculation", t => t.Calculation_Id, cascadeDelete: true)
                .Index(t => t.AddonServicesLink_Id)
                .Index(t => t.Calculation_Id);
            
            CreateTable(
                "dbo.CalculationChild",
                c => new
                    {
                        Calculation_Id = c.Long(nullable: false),
                        Child_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Calculation_Id, t.Child_Id })
                .ForeignKey("dbo.Calculation", t => t.Calculation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Child", t => t.Child_Id, cascadeDelete: true)
                .Index(t => t.Calculation_Id)
                .Index(t => t.Child_Id);
            
            CreateTable(
                "dbo.CalculationTicket",
                c => new
                    {
                        Calculation_Id = c.Long(nullable: false),
                        Ticket_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Calculation_Id, t.Ticket_Id })
                .ForeignKey("dbo.Calculation", t => t.Calculation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.Ticket_Id, cascadeDelete: true)
                .Index(t => t.Calculation_Id)
                .Index(t => t.Ticket_Id);
            
            CreateTable(
                "dbo.ApplicantCalculation",
                c => new
                    {
                        Applicant_Id = c.Long(nullable: false),
                        Calculation_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Applicant_Id, t.Calculation_Id })
                .ForeignKey("dbo.Applicant", t => t.Applicant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Calculation", t => t.Calculation_Id, cascadeDelete: true)
                .Index(t => t.Applicant_Id)
                .Index(t => t.Calculation_Id);
            
            AddColumn("dbo.Applicant", "OfferInRequestId", c => c.Long());
            AddColumn("dbo.Child", "OfferInRequestId", c => c.Long());
            CreateIndex("dbo.Applicant", "OfferInRequestId");
            CreateIndex("dbo.Child", "OfferInRequestId");
            AddForeignKey("dbo.Child", "OfferInRequestId", "dbo.OfferInRequest", "Id");
            AddForeignKey("dbo.Applicant", "OfferInRequestId", "dbo.OfferInRequest", "Id");
            DropColumn("dbo.Applicant", "RoomRatesId");
            DropColumn("dbo.Child", "RoomRatesId");
            DropColumn("dbo.Calculation", "ChildId");
            DropColumn("dbo.Calculation", "ApplicantId");
            DropColumn("dbo.Calculation", "AddonServicesLinkId");
            DropColumn("dbo.Calculation", "TicketId");
            DropTable("dbo.RequestRoomRates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RequestRoomRates",
                c => new
                    {
                        Request_Id = c.Long(nullable: false),
                        RoomRates_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Request_Id, t.RoomRates_Id });
            
            AddColumn("dbo.Calculation", "TicketId", c => c.Long());
            AddColumn("dbo.Calculation", "AddonServicesLinkId", c => c.Long());
            AddColumn("dbo.Calculation", "ApplicantId", c => c.Long());
            AddColumn("dbo.Calculation", "ChildId", c => c.Long());
            AddColumn("dbo.Child", "RoomRatesId", c => c.Long());
            AddColumn("dbo.Applicant", "RoomRatesId", c => c.Long());
            DropForeignKey("dbo.Applicant", "OfferInRequestId", "dbo.OfferInRequest");
            DropForeignKey("dbo.ApplicantCalculation", "Calculation_Id", "dbo.Calculation");
            DropForeignKey("dbo.ApplicantCalculation", "Applicant_Id", "dbo.Applicant");
            DropForeignKey("dbo.CalculationTicket", "Ticket_Id", "dbo.Ticket");
            DropForeignKey("dbo.CalculationTicket", "Calculation_Id", "dbo.Calculation");
            DropForeignKey("dbo.CalculationChild", "Child_Id", "dbo.Child");
            DropForeignKey("dbo.CalculationChild", "Calculation_Id", "dbo.Calculation");
            DropForeignKey("dbo.Child", "OfferInRequestId", "dbo.OfferInRequest");
            DropForeignKey("dbo.OfferInRequest", "RoomRatesId", "dbo.RoomRates");
            DropForeignKey("dbo.OfferInRequest", "RequestId", "dbo.Request");
            DropForeignKey("dbo.AddonServicesLinkCalculation", "Calculation_Id", "dbo.Calculation");
            DropForeignKey("dbo.AddonServicesLinkCalculation", "AddonServicesLink_Id", "dbo.AddonServicesLink");
            DropIndex("dbo.ApplicantCalculation", new[] { "Calculation_Id" });
            DropIndex("dbo.ApplicantCalculation", new[] { "Applicant_Id" });
            DropIndex("dbo.CalculationTicket", new[] { "Ticket_Id" });
            DropIndex("dbo.CalculationTicket", new[] { "Calculation_Id" });
            DropIndex("dbo.CalculationChild", new[] { "Child_Id" });
            DropIndex("dbo.CalculationChild", new[] { "Calculation_Id" });
            DropIndex("dbo.AddonServicesLinkCalculation", new[] { "Calculation_Id" });
            DropIndex("dbo.AddonServicesLinkCalculation", new[] { "AddonServicesLink_Id" });
            DropIndex("dbo.OfferInRequest", new[] { "RoomRatesId" });
            DropIndex("dbo.OfferInRequest", new[] { "RequestId" });
            DropIndex("dbo.Child", new[] { "OfferInRequestId" });
            DropIndex("dbo.Applicant", new[] { "OfferInRequestId" });
            DropColumn("dbo.Child", "OfferInRequestId");
            DropColumn("dbo.Applicant", "OfferInRequestId");
            DropTable("dbo.ApplicantCalculation");
            DropTable("dbo.CalculationTicket");
            DropTable("dbo.CalculationChild");
            DropTable("dbo.AddonServicesLinkCalculation");
            DropTable("dbo.OfferInRequest");
            CreateIndex("dbo.RequestRoomRates", "RoomRates_Id");
            CreateIndex("dbo.RequestRoomRates", "Request_Id");
            CreateIndex("dbo.Calculation", "TicketId");
            CreateIndex("dbo.Calculation", "AddonServicesLinkId");
            CreateIndex("dbo.Calculation", "ApplicantId");
            CreateIndex("dbo.Calculation", "ChildId");
            CreateIndex("dbo.Child", "RoomRatesId");
            CreateIndex("dbo.Applicant", "RoomRatesId");
            AddForeignKey("dbo.Calculation", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.Calculation", "ChildId", "dbo.Child", "Id");
            AddForeignKey("dbo.Calculation", "ApplicantId", "dbo.Applicant", "Id");
            AddForeignKey("dbo.Calculation", "AddonServicesLinkId", "dbo.AddonServicesLink", "Id");
            AddForeignKey("dbo.Applicant", "RoomRatesId", "dbo.RoomRates", "Id");
            AddForeignKey("dbo.Child", "RoomRatesId", "dbo.RoomRates", "Id");
            AddForeignKey("dbo.RequestRoomRates", "RoomRates_Id", "dbo.RoomRates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestRoomRates", "Request_Id", "dbo.Request", "Id", cascadeDelete: true);
        }
    }
}
