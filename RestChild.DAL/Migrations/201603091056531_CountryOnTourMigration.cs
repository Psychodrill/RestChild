namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryOnTourMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CountryTour", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.CountryTour", "Tour_Id", "dbo.Tour");
            DropIndex("dbo.CountryTour", new[] { "Country_Id" });
            DropIndex("dbo.CountryTour", new[] { "Tour_Id" });
            CreateTable(
                "dbo.TourCountry",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsMain = c.Boolean(nullable: false),
                        CountryId = c.Long(),
                        TourId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId)
                .Index(t => t.TourId);
            
            AddColumn("dbo.Request", "ProcentPrepaid", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.AddonServicesLink", "ManualPrice", c => c.Boolean(nullable: false));
            DropTable("dbo.CountryTour");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CountryTour",
                c => new
                    {
                        Country_Id = c.Long(nullable: false),
                        Tour_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.Tour_Id });
            
            DropForeignKey("dbo.TourCountry", "CountryId", "dbo.Country");
            DropForeignKey("dbo.TourCountry", "TourId", "dbo.Tour");
            DropIndex("dbo.TourCountry", new[] { "TourId" });
            DropIndex("dbo.TourCountry", new[] { "CountryId" });
            DropColumn("dbo.AddonServicesLink", "ManualPrice");
            DropColumn("dbo.Request", "ProcentPrepaid");
            DropTable("dbo.TourCountry");
            CreateIndex("dbo.CountryTour", "Tour_Id");
            CreateIndex("dbo.CountryTour", "Country_Id");
            AddForeignKey("dbo.CountryTour", "Tour_Id", "dbo.Tour", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CountryTour", "Country_Id", "dbo.Country", "Id", cascadeDelete: true);
        }
    }
}
