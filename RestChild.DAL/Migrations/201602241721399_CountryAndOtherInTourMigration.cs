namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryAndOtherInTourMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryDiningOptions",
                c => new
                    {
                        Country_Id = c.Long(nullable: false),
                        DiningOptions_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.DiningOptions_Id })
                .ForeignKey("dbo.Country", t => t.Country_Id, cascadeDelete: true)
                .ForeignKey("dbo.DiningOptions", t => t.DiningOptions_Id, cascadeDelete: true)
                .Index(t => t.Country_Id)
                .Index(t => t.DiningOptions_Id);
            
            CreateTable(
                "dbo.CountryTour",
                c => new
                    {
                        Country_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.Tour_Id })
                .ForeignKey("dbo.Country", t => t.Country_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Country_Id)
                .Index(t => t.Tour_Id);
            
            CreateTable(
                "dbo.AddonServicesPlaceOfRest",
                c => new
                    {
                        AddonServices_Id = c.Long(nullable: false),
                        PlaceOfRest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AddonServices_Id, t.PlaceOfRest_Id })
                .ForeignKey("dbo.AddonServices", t => t.AddonServices_Id, cascadeDelete: true)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRest_Id, cascadeDelete: true)
                .Index(t => t.AddonServices_Id)
                .Index(t => t.PlaceOfRest_Id);
            
            AddColumn("dbo.TypeOfRest", "NumberCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.Hotels", "SubHotelTypeId", c => c.Long());
            AddColumn("dbo.HotelType", "ParentId", c => c.Long());
            CreateIndex("dbo.Hotels", "SubHotelTypeId");
            CreateIndex("dbo.HotelType", "ParentId");
            AddForeignKey("dbo.HotelType", "ParentId", "dbo.HotelType", "Id");
            AddForeignKey("dbo.Hotels", "SubHotelTypeId", "dbo.HotelType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddonServicesPlaceOfRest", "PlaceOfRest_Id", "dbo.PlaceOfRest");
            DropForeignKey("dbo.AddonServicesPlaceOfRest", "AddonServices_Id", "dbo.AddonServices");
            DropForeignKey("dbo.CountryTour", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.CountryTour", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.CountryDiningOptions", "DiningOptions_Id", "dbo.DiningOptions");
            DropForeignKey("dbo.CountryDiningOptions", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Hotels", "SubHotelTypeId", "dbo.HotelType");
            DropForeignKey("dbo.HotelType", "ParentId", "dbo.HotelType");
            DropIndex("dbo.AddonServicesPlaceOfRest", new[] { "PlaceOfRest_Id" });
            DropIndex("dbo.AddonServicesPlaceOfRest", new[] { "AddonServices_Id" });
            DropIndex("dbo.CountryTour", new[] { "Tour_Id" });
            DropIndex("dbo.CountryTour", new[] { "Country_Id" });
            DropIndex("dbo.CountryDiningOptions", new[] { "DiningOptions_Id" });
            DropIndex("dbo.CountryDiningOptions", new[] { "Country_Id" });
            DropIndex("dbo.HotelType", new[] { "ParentId" });
            DropIndex("dbo.Hotels", new[] { "SubHotelTypeId" });
            DropColumn("dbo.HotelType", "ParentId");
            DropColumn("dbo.Hotels", "SubHotelTypeId");
            DropColumn("dbo.TypeOfRest", "NumberCode");
            DropTable("dbo.AddonServicesPlaceOfRest");
            DropTable("dbo.CountryTour");
            DropTable("dbo.CountryDiningOptions");
        }
    }
}
