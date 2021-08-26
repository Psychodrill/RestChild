namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServicePropertysMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventGeography",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        SortOrder = c.Int(),
                        Description = c.String(),
                        AddonServicesId = c.Long(),
                        CityId = c.Long(),
                        TourId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .Index(t => t.AddonServicesId)
                .Index(t => t.CityId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.RequestService",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(),
                        AddonServicesId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddonServices", t => t.AddonServicesId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.RequestId)
                .Index(t => t.AddonServicesId);
            
            CreateTable(
                "dbo.TypeOfRestSubtype",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false),
                        TypeOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .Index(t => t.TypeOfRestId);
            
            AddColumn("dbo.Tour", "NeedApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "AnnouncementEvent", c => c.String());
            AddColumn("dbo.Tour", "ConditionsForAccompanying", c => c.String());
            AddColumn("dbo.Tour", "SizeMin", c => c.Int());
            AddColumn("dbo.Tour", "SizeMax", c => c.Int());
            AddColumn("dbo.Tour", "BaseServiceId", c => c.Long());
            AddColumn("dbo.Tour", "TypeOfRestSubtypeId", c => c.Long());
            AddColumn("dbo.AddonServicesLink", "PriceInternal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.AddonServicesLink", "Commentary", c => c.String());
            AddColumn("dbo.AddonServicesLink", "ServiceId", c => c.Long());
            AddColumn("dbo.AddonServices", "NeedApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.AddonServices", "DateBookingFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServices", "DateBookingTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AddonServices", "AnnouncementEvent", c => c.String());
            AddColumn("dbo.AddonServices", "ConditionsForAccompanying", c => c.String());
            AddColumn("dbo.AddonServices", "SizeMin", c => c.Int());
            AddColumn("dbo.AddonServices", "SizeMax", c => c.Int());
            AddColumn("dbo.AddonServices", "ContractId", c => c.Long());
            AddColumn("dbo.AddonServices", "TypeOfRoomsId", c => c.Long());
            AddColumn("dbo.AddonServices", "TypeOfRestSubtypeId", c => c.Long());
            AddColumn("dbo.TourAccommodation", "NeedApprove", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourAccommodation", "EventGeographyId", c => c.Long());
            AddColumn("dbo.Request", "TypeOfRestSubtypeId", c => c.Long());
            AddColumn("dbo.Request", "BaseServiceId", c => c.Long());
            AddColumn("dbo.RequestAccommodation", "EventGeographyId", c => c.Long());
            CreateIndex("dbo.Tour", "BaseServiceId");
            CreateIndex("dbo.Tour", "TypeOfRestSubtypeId");
            CreateIndex("dbo.AddonServicesLink", "ServiceId");
            CreateIndex("dbo.AddonServices", "ContractId");
            CreateIndex("dbo.AddonServices", "TypeOfRoomsId");
            CreateIndex("dbo.AddonServices", "TypeOfRestSubtypeId");
            CreateIndex("dbo.TourAccommodation", "EventGeographyId");
            CreateIndex("dbo.Request", "TypeOfRestSubtypeId");
            CreateIndex("dbo.Request", "BaseServiceId");
            CreateIndex("dbo.RequestAccommodation", "EventGeographyId");
            AddForeignKey("dbo.TourAccommodation", "EventGeographyId", "dbo.EventGeography", "Id");
            AddForeignKey("dbo.Request", "BaseServiceId", "dbo.AddonServices", "Id");
            AddForeignKey("dbo.RequestAccommodation", "EventGeographyId", "dbo.EventGeography", "Id");
            AddForeignKey("dbo.Request", "TypeOfRestSubtypeId", "dbo.TypeOfRestSubtype", "Id");
            AddForeignKey("dbo.AddonServices", "ContractId", "dbo.Contract", "Id");
            AddForeignKey("dbo.AddonServices", "TypeOfRestSubtypeId", "dbo.TypeOfRestSubtype", "Id");
            AddForeignKey("dbo.AddonServices", "TypeOfRoomsId", "dbo.TypeOfRooms", "Id");
            AddForeignKey("dbo.AddonServicesLink", "ServiceId", "dbo.RequestService", "Id");
            AddForeignKey("dbo.Tour", "BaseServiceId", "dbo.AddonServices", "Id");
            AddForeignKey("dbo.Tour", "TypeOfRestSubtypeId", "dbo.TypeOfRestSubtype", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "TypeOfRestSubtypeId", "dbo.TypeOfRestSubtype");
            DropForeignKey("dbo.EventGeography", "TourId", "dbo.Tour");
            DropForeignKey("dbo.Tour", "BaseServiceId", "dbo.AddonServices");
            DropForeignKey("dbo.AddonServicesLink", "ServiceId", "dbo.RequestService");
            DropForeignKey("dbo.AddonServices", "TypeOfRoomsId", "dbo.TypeOfRooms");
            DropForeignKey("dbo.AddonServices", "TypeOfRestSubtypeId", "dbo.TypeOfRestSubtype");
            DropForeignKey("dbo.EventGeography", "AddonServicesId", "dbo.AddonServices");
            DropForeignKey("dbo.AddonServices", "ContractId", "dbo.Contract");
            DropForeignKey("dbo.Request", "TypeOfRestSubtypeId", "dbo.TypeOfRestSubtype");
            DropForeignKey("dbo.TypeOfRestSubtype", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.RequestService", "RequestId", "dbo.Request");
            DropForeignKey("dbo.RequestService", "AddonServicesId", "dbo.AddonServices");
            DropForeignKey("dbo.RequestAccommodation", "EventGeographyId", "dbo.EventGeography");
            DropForeignKey("dbo.Request", "BaseServiceId", "dbo.AddonServices");
            DropForeignKey("dbo.TourAccommodation", "EventGeographyId", "dbo.EventGeography");
            DropForeignKey("dbo.EventGeography", "CityId", "dbo.City");
            DropIndex("dbo.TypeOfRestSubtype", new[] { "TypeOfRestId" });
            DropIndex("dbo.RequestService", new[] { "AddonServicesId" });
            DropIndex("dbo.RequestService", new[] { "RequestId" });
            DropIndex("dbo.RequestAccommodation", new[] { "EventGeographyId" });
            DropIndex("dbo.Request", new[] { "BaseServiceId" });
            DropIndex("dbo.Request", new[] { "TypeOfRestSubtypeId" });
            DropIndex("dbo.EventGeography", new[] { "TourId" });
            DropIndex("dbo.EventGeography", new[] { "CityId" });
            DropIndex("dbo.EventGeography", new[] { "AddonServicesId" });
            DropIndex("dbo.TourAccommodation", new[] { "EventGeographyId" });
            DropIndex("dbo.AddonServices", new[] { "TypeOfRestSubtypeId" });
            DropIndex("dbo.AddonServices", new[] { "TypeOfRoomsId" });
            DropIndex("dbo.AddonServices", new[] { "ContractId" });
            DropIndex("dbo.AddonServicesLink", new[] { "ServiceId" });
            DropIndex("dbo.Tour", new[] { "TypeOfRestSubtypeId" });
            DropIndex("dbo.Tour", new[] { "BaseServiceId" });
            DropColumn("dbo.RequestAccommodation", "EventGeographyId");
            DropColumn("dbo.Request", "BaseServiceId");
            DropColumn("dbo.Request", "TypeOfRestSubtypeId");
            DropColumn("dbo.TourAccommodation", "EventGeographyId");
            DropColumn("dbo.TourAccommodation", "NeedApprove");
            DropColumn("dbo.AddonServices", "TypeOfRestSubtypeId");
            DropColumn("dbo.AddonServices", "TypeOfRoomsId");
            DropColumn("dbo.AddonServices", "ContractId");
            DropColumn("dbo.AddonServices", "SizeMax");
            DropColumn("dbo.AddonServices", "SizeMin");
            DropColumn("dbo.AddonServices", "ConditionsForAccompanying");
            DropColumn("dbo.AddonServices", "AnnouncementEvent");
            DropColumn("dbo.AddonServices", "DateBookingTo");
            DropColumn("dbo.AddonServices", "DateBookingFrom");
            DropColumn("dbo.AddonServices", "NeedApprove");
            DropColumn("dbo.AddonServicesLink", "ServiceId");
            DropColumn("dbo.AddonServicesLink", "Commentary");
            DropColumn("dbo.AddonServicesLink", "PriceInternal");
            DropColumn("dbo.Tour", "TypeOfRestSubtypeId");
            DropColumn("dbo.Tour", "BaseServiceId");
            DropColumn("dbo.Tour", "SizeMax");
            DropColumn("dbo.Tour", "SizeMin");
            DropColumn("dbo.Tour", "ConditionsForAccompanying");
            DropColumn("dbo.Tour", "AnnouncementEvent");
            DropColumn("dbo.Tour", "NeedApprove");
            DropTable("dbo.TypeOfRestSubtype");
            DropTable("dbo.RequestService");
            DropTable("dbo.EventGeography");
        }
    }
}
