namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommercialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 1000),
                        FullName = c.String(nullable: false, maxLength: 1000),
						SymbolCode2 = c.String(nullable: false, maxLength: 10),
                        SymbolCode3 = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accommodation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Adult = c.Int(nullable: false),
                        Name = c.String(maxLength: 1000),
                        HotelId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.AccommodationChildren",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AgeFrom = c.Int(nullable: false),
                        AgeTo = c.Int(nullable: false),
                        CountChildren = c.Int(nullable: false),
                        AccommodationId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accommodation", t => t.AccommodationId)
                .Index(t => t.AccommodationId);
            
            CreateTable(
                "dbo.DiningOptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        HotelId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.RoomRates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        TypeOfRoomsId = c.Long(),
                        DiningOptionsId = c.Long(),
                        AccommodationId = c.Long(),
                        YearOfRestId = c.Long(),
                        HotelId = c.Long(),
                        TourId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accommodation", t => t.AccommodationId)
                .ForeignKey("dbo.DiningOptions", t => t.DiningOptionsId)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.TypeOfRoomsId)
                .Index(t => t.DiningOptionsId)
                .Index(t => t.AccommodationId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HotelId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.AddonServicesLink",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(precision: 18, scale: 2),
                        CountService = c.Decimal(precision: 18, scale: 2),
                        AddonServicesId = c.Long(),
                        RequestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.AddonServicesId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.AddonServices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 1000),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Requared = c.Boolean(),
                        GeneralService = c.Boolean(nullable: false),
                        ForForeign = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TourId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.TourId);
            
            AddColumn("dbo.PlaceOfRest", "CountryId", c => c.Long());
            AddColumn("dbo.TypeOfRest", "Commercial", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "IsAddon", c => c.Boolean(nullable: false));
            AddColumn("dbo.City", "CountryId", c => c.Long());
            AddColumn("dbo.Request", "ParentRequestId", c => c.Long());
            CreateIndex("dbo.PlaceOfRest", "CountryId");
            CreateIndex("dbo.City", "CountryId");
            CreateIndex("dbo.Request", "ParentRequestId");
            AddForeignKey("dbo.PlaceOfRest", "CountryId", "dbo.Country", "Id");
            AddForeignKey("dbo.City", "CountryId", "dbo.Country", "Id");
            AddForeignKey("dbo.Request", "ParentRequestId", "dbo.Request", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomRates", "TourId", "dbo.Tour");
            DropForeignKey("dbo.AddonServicesLink", "RequestId", "dbo.Request");
            DropForeignKey("dbo.AddonServicesLink", "AddonServicesId", "dbo.AddonServices");
            DropForeignKey("dbo.AddonServices", "TourId", "dbo.Tour");
            DropForeignKey("dbo.Request", "ParentRequestId", "dbo.Request");
            DropForeignKey("dbo.RoomRates", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.RoomRates", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.RoomRates", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.RoomRates", "DiningOptionsId", "dbo.DiningOptions");
            DropForeignKey("dbo.RoomRates", "AccommodationId", "dbo.Accommodation");
            DropForeignKey("dbo.DiningOptions", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.City", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Accommodation", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.AccommodationChildren", "AccommodationId", "dbo.Accommodation");
            DropForeignKey("dbo.PlaceOfRest", "CountryId", "dbo.Country");
            DropIndex("dbo.AddonServices", new[] { "TourId" });
            DropIndex("dbo.AddonServicesLink", new[] { "RequestId" });
            DropIndex("dbo.AddonServicesLink", new[] { "AddonServicesId" });
            DropIndex("dbo.Request", new[] { "ParentRequestId" });
            DropIndex("dbo.RoomRates", new[] { "TourId" });
            DropIndex("dbo.RoomRates", new[] { "HotelId" });
            DropIndex("dbo.RoomRates", new[] { "YearOfRestId" });
            DropIndex("dbo.RoomRates", new[] { "AccommodationId" });
            DropIndex("dbo.RoomRates", new[] { "DiningOptionsId" });
            DropIndex("dbo.RoomRates", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.DiningOptions", new[] { "HotelId" });
            DropIndex("dbo.City", new[] { "CountryId" });
            DropIndex("dbo.AccommodationChildren", new[] { "AccommodationId" });
            DropIndex("dbo.Accommodation", new[] { "HotelId" });
            DropIndex("dbo.PlaceOfRest", new[] { "CountryId" });
            DropColumn("dbo.Request", "ParentRequestId");
            DropColumn("dbo.City", "CountryId");
            DropColumn("dbo.TypeOfRest", "IsAddon");
            DropColumn("dbo.TypeOfRest", "Commercial");
            DropColumn("dbo.PlaceOfRest", "CountryId");
            DropTable("dbo.AddonServices");
            DropTable("dbo.AddonServicesLink");
            DropTable("dbo.RoomRates");
            DropTable("dbo.DiningOptions");
            DropTable("dbo.AccommodationChildren");
            DropTable("dbo.Accommodation");
            DropTable("dbo.Country");
        }
    }
}
