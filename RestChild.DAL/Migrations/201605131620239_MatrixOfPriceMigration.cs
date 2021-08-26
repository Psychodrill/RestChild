namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatrixOfPriceMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AgeFrom = c.Int(),
                        AgtTo = c.Int(),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Price = c.Decimal(nullable: false, precision: 32, scale: 4),
                        PriceInternal = c.Decimal(nullable: false, precision: 32, scale: 4),
                        DiningOptionsId = c.Long(),
                        TypeOfRoomsId = c.Long(),
                        YearOfRestId = c.Long(),
                        AccommodationId = c.Long(),
                        HotelId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accommodation", t => t.AccommodationId)
                .ForeignKey("dbo.DiningOptions", t => t.DiningOptionsId)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .Index(t => t.DiningOptionsId)
                .Index(t => t.TypeOfRoomsId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.AccommodationId)
                .Index(t => t.HotelId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Organization", "CountInTour", c => c.Int());
            AddColumn("dbo.Hotels", "IsLok", c => c.Boolean(nullable: false));
            AddColumn("dbo.Hotels", "DescriptionHtml", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelPrice", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.HotelPrice", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.HotelPrice", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.HotelPrice", "DiningOptionsId", "dbo.DiningOptions");
            DropForeignKey("dbo.HotelPrice", "AccommodationId", "dbo.Accommodation");
            DropIndex("dbo.HotelPrice", new[] { "EidSendStatus" });
            DropIndex("dbo.HotelPrice", new[] { "Eid" });
            DropIndex("dbo.HotelPrice", new[] { "HotelId" });
            DropIndex("dbo.HotelPrice", new[] { "AccommodationId" });
            DropIndex("dbo.HotelPrice", new[] { "YearOfRestId" });
            DropIndex("dbo.HotelPrice", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.HotelPrice", new[] { "DiningOptionsId" });
            DropColumn("dbo.Hotels", "DescriptionHtml");
            DropColumn("dbo.Hotels", "IsLok");
            DropColumn("dbo.Organization", "CountInTour");
            DropTable("dbo.HotelPrice");
        }
    }
}
