namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationLimitsNewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LimitOnOrganization", "TimeOfRestId", c => c.Long());
            AddColumn("dbo.LimitOnOrganization", "PlaceOfRestId", c => c.Long());
            AddColumn("dbo.LimitOnOrganization", "TourId", c => c.Long());
            AddColumn("dbo.Tour", "LimitOnVedomstvoId", c => c.Long());
            CreateIndex("dbo.LimitOnOrganization", "TimeOfRestId");
            CreateIndex("dbo.LimitOnOrganization", "PlaceOfRestId");
            CreateIndex("dbo.LimitOnOrganization", "TourId");
            CreateIndex("dbo.Tour", "LimitOnVedomstvoId");
            AddForeignKey("dbo.LimitOnOrganization", "PlaceOfRestId", "dbo.PlaceOfRest", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "TimeOfRestId", "dbo.TimeOfRest", "Id");
            AddForeignKey("dbo.Tour", "LimitOnVedomstvoId", "dbo.LimitOnVedomstvo", "Id");
            AddForeignKey("dbo.LimitOnOrganization", "TourId", "dbo.Tour", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LimitOnOrganization", "TourId", "dbo.Tour");
            DropForeignKey("dbo.Tour", "LimitOnVedomstvoId", "dbo.LimitOnVedomstvo");
            DropForeignKey("dbo.LimitOnOrganization", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.LimitOnOrganization", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropIndex("dbo.Tour", new[] { "LimitOnVedomstvoId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "TourId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "PlaceOfRestId" });
            DropIndex("dbo.LimitOnOrganization", new[] { "TimeOfRestId" });
            DropColumn("dbo.Tour", "LimitOnVedomstvoId");
            DropColumn("dbo.LimitOnOrganization", "TourId");
            DropColumn("dbo.LimitOnOrganization", "PlaceOfRestId");
            DropColumn("dbo.LimitOnOrganization", "TimeOfRestId");
        }
    }
}
