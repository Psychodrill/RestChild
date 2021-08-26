namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourToProductSecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "GroupedTimeOfRestId", c => c.Long());
            AddColumn("dbo.RoomRates", "PriceTotal", c => c.Decimal(precision: 32, scale: 4));
            AddColumn("dbo.Request", "GroupedTimeOfRestId", c => c.Long());
            AddColumn("dbo.TourVolume", "EventDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Tour", "GroupedTimeOfRestId");
            CreateIndex("dbo.Request", "GroupedTimeOfRestId");
            AddForeignKey("dbo.Request", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest", "Id");
            AddForeignKey("dbo.Tour", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest");
            DropForeignKey("dbo.Request", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest");
            DropIndex("dbo.Request", new[] { "GroupedTimeOfRestId" });
            DropIndex("dbo.Tour", new[] { "GroupedTimeOfRestId" });
            DropColumn("dbo.TourVolume", "EventDate");
            DropColumn("dbo.Request", "GroupedTimeOfRestId");
            DropColumn("dbo.RoomRates", "PriceTotal");
            DropColumn("dbo.Tour", "GroupedTimeOfRestId");
        }
    }
}
