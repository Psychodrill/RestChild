namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourTransportMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourTransportPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AgeFrom = c.Int(),
                        AgeTo = c.Int(),
                        Price = c.Decimal(precision: 32, scale: 4),
                        PriceInternal = c.Decimal(precision: 32, scale: 4),
                        TourTransportId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourTransport", t => t.TourTransportId)
                .Index(t => t.TourTransportId);
            
            AddColumn("dbo.Request", "ParentListOfChildId", c => c.Long());
            AddColumn("dbo.TourTransport", "DateOfDeparture", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TourTransport", "DateOfArrival", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TourTransport", "PlaceOfDeparture", c => c.String());
            AddColumn("dbo.TourTransport", "PlaceOfArrival", c => c.String());
            AddColumn("dbo.TourTransport", "NeedApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourTransport", "CityOfDepartureId", c => c.Long());
            AddColumn("dbo.TourTransport", "CityOfArrivalId", c => c.Long());
            CreateIndex("dbo.Request", "ParentListOfChildId");
            CreateIndex("dbo.TourTransport", "CityOfDepartureId");
            CreateIndex("dbo.TourTransport", "CityOfArrivalId");
            AddForeignKey("dbo.Request", "ParentListOfChildId", "dbo.ListOfChilds", "Id");
            AddForeignKey("dbo.TourTransport", "CityOfArrivalId", "dbo.City", "Id");
            AddForeignKey("dbo.TourTransport", "CityOfDepartureId", "dbo.City", "Id");
            DropColumn("dbo.TourTransport", "AgeFrom");
            DropColumn("dbo.TourTransport", "AgeTo");
            DropColumn("dbo.TourTransport", "Price");
            DropColumn("dbo.TourTransport", "PriceInternal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourTransport", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.TourTransport", "Price", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.TourTransport", "AgeTo", c => c.Int());
            AddColumn("dbo.TourTransport", "AgeFrom", c => c.Int());
            DropForeignKey("dbo.TourTransportPrice", "TourTransportId", "dbo.TourTransport");
            DropForeignKey("dbo.TourTransport", "CityOfDepartureId", "dbo.City");
            DropForeignKey("dbo.TourTransport", "CityOfArrivalId", "dbo.City");
            DropForeignKey("dbo.Request", "ParentListOfChildId", "dbo.ListOfChilds");
            DropIndex("dbo.TourTransportPrice", new[] { "TourTransportId" });
            DropIndex("dbo.TourTransport", new[] { "CityOfArrivalId" });
            DropIndex("dbo.TourTransport", new[] { "CityOfDepartureId" });
            DropIndex("dbo.Request", new[] { "ParentListOfChildId" });
            DropColumn("dbo.TourTransport", "CityOfArrivalId");
            DropColumn("dbo.TourTransport", "CityOfDepartureId");
            DropColumn("dbo.TourTransport", "NeedApprove");
            DropColumn("dbo.TourTransport", "PlaceOfArrival");
            DropColumn("dbo.TourTransport", "PlaceOfDeparture");
            DropColumn("dbo.TourTransport", "DateOfArrival");
            DropColumn("dbo.TourTransport", "DateOfDeparture");
            DropColumn("dbo.Request", "ParentListOfChildId");
            DropTable("dbo.TourTransportPrice");
        }
    }
}
