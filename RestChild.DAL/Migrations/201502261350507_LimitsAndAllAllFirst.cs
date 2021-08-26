namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LimitsAndAllAllFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LimitStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YearOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Address = c.String(nullable: false, maxLength: 1000),
                        NameOrganization = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        Fax = c.String(maxLength: 1000),
                        Email = c.String(),
                        Url = c.String(),
                        Head = c.String(),
                        HeadPosition = c.String(),
                        DrivingDirections = c.String(),
                        NumberHousing = c.String(maxLength: 1000),
                        Squere = c.Decimal(precision: 18, scale: 2),
                        MedicalOfficeAvailability = c.Boolean(nullable: false),
                        OutdoorPondAvailability = c.Boolean(nullable: false),
                        OutdoorPondName = c.String(),
                        PoolAvailability = c.Boolean(nullable: false),
                        SecurityInformation = c.String(),
                        ComputerClassAvailability = c.Boolean(nullable: false),
                        CinimaAvailability = c.Boolean(nullable: false),
                        CinimaTimetable = c.String(),
                        GymAvailability = c.Boolean(nullable: false),
                        LibraryAvailability = c.Boolean(nullable: false),
                        LibraryTimetable = c.String(),
                        PlaceOfRestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .Index(t => t.PlaceOfRestId);
            
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Descr = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DateIncome = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateOutcome = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TypeOfRestId = c.Long(),
                        TimeOfRestId = c.Long(),
                        YearOfRestId = c.Long(),
                        SubjectOfRestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectOfRest", t => t.SubjectOfRestId)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRestId)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.TimeOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.SubjectOfRestId);
            
            CreateTable(
                "dbo.TourVolume",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountBasePlace = c.Int(nullable: false),
                        CountAddonPlace = c.Int(nullable: false),
                        TypeOfRoomsId = c.Long(),
                        TourId = c.Long(),
                        HotelsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelsId)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .ForeignKey("dbo.TypeOfRooms", t => t.TypeOfRoomsId)
                .Index(t => t.TypeOfRoomsId)
                .Index(t => t.TourId)
                .Index(t => t.HotelsId);
            
            CreateTable(
                "dbo.TypeOfRooms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        HaveFurniture = c.Boolean(nullable: false),
                        HaveBalcony = c.Boolean(nullable: false),
                        HaveTv = c.Boolean(nullable: false),
                        HaveBath = c.Boolean(nullable: false),
                        HaveSatelliteTv = c.Boolean(nullable: false),
                        HaveShower = c.Boolean(nullable: false),
                        HaveLocalTv = c.Boolean(nullable: false),
                        HaveHairDryer = c.Boolean(nullable: false),
                        HaveRadio = c.Boolean(nullable: false),
                        HaveWc = c.Boolean(nullable: false),
                        HavePhone = c.Boolean(nullable: false),
                        HaveBidet = c.Boolean(nullable: false),
                        HaveBar = c.Boolean(nullable: false),
                        HaveAirConditioning = c.Boolean(nullable: false),
                        HaveSafe = c.Boolean(nullable: false),
                        HaveKitchen = c.Boolean(nullable: false),
                        HaveRefrigerator = c.Boolean(nullable: false),
                        HotelsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelsId)
                .Index(t => t.HotelsId);
            
            AddColumn("dbo.LimitOnOrganization", "LimitStatusId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "LimitStatusId", c => c.Long());
            AddColumn("dbo.LimitOnVedomstvo", "YearOfRestId", c => c.Long());
            AddColumn("dbo.TimeOfRest", "YearOfRestId", c => c.Long());
            CreateIndex("dbo.LimitOnOrganization", "LimitStatusId");
            CreateIndex("dbo.LimitOnVedomstvo", "LimitStatusId");
            CreateIndex("dbo.LimitOnVedomstvo", "YearOfRestId");
            CreateIndex("dbo.TimeOfRest", "YearOfRestId");
            AddForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus", "Id");
            AddForeignKey("dbo.LimitOnVedomstvo", "YearOfRestId", "dbo.YearOfRest", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "LimitStatusId", "dbo.LimitStatus", "Id");
            AddForeignKey("dbo.TimeOfRest", "YearOfRestId", "dbo.YearOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourVolume", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.TypeOfRooms", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.TourVolume", "TourId", "dbo.Tour");
            DropForeignKey("dbo.TourVolume", "HotelsId", "dbo.Hotels");
            DropForeignKey("dbo.Tour", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.Tour", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.Tour", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.Tour", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropForeignKey("dbo.Hotels", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.TimeOfRest", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.LimitOnOrganization", "LimitStatusId", "dbo.LimitStatus");
            DropForeignKey("dbo.LimitOnVedomstvo", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.LimitOnVedomstvo", "LimitStatusId", "dbo.LimitStatus");
            DropIndex("dbo.TypeOfRooms", new[] { "HotelsId" });
            DropIndex("dbo.TourVolume", new[] { "HotelsId" });
            DropIndex("dbo.TourVolume", new[] { "TourId" });
            DropIndex("dbo.TourVolume", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.Tour", new[] { "SubjectOfRestId" });
            DropIndex("dbo.Tour", new[] { "YearOfRestId" });
            DropIndex("dbo.Tour", new[] { "TimeOfRestId" });
            DropIndex("dbo.Tour", new[] { "TypeOfRestId" });
            DropIndex("dbo.Hotels", new[] { "PlaceOfRestId" });
            DropIndex("dbo.TimeOfRest", new[] { "YearOfRestId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "YearOfRestId" });
            DropIndex("dbo.LimitOnVedomstvo", new[] { "LimitStatusId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "LimitStatusId" });
            DropColumn("dbo.TimeOfRest", "YearOfRestId");
            DropColumn("dbo.LimitOnVedomstvo", "YearOfRestId");
            DropColumn("dbo.LimitOnVedomstvo", "LimitStatusId");
            DropColumn("dbo.LimitOnOrganization", "LimitStatusId");
            DropTable("dbo.TypeOfRooms");
            DropTable("dbo.TourVolume");
            DropTable("dbo.Tour");
            DropTable("dbo.Hotels");
            DropTable("dbo.YearOfRest");
            DropTable("dbo.LimitStatus");
        }
    }
}
