namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlacesAndFields : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FileOfTour", name: "HotelId", newName: "TourId");
            RenameIndex(table: "dbo.FileOfTour", name: "IX_HotelId", newName: "IX_TourId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FileOfTour", name: "IX_TourId", newName: "IX_HotelId");
            RenameColumn(table: "dbo.FileOfTour", name: "TourId", newName: "HotelId");
        }
    }
}
