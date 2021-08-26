namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingNewFieldMigrationAndRel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "TourId", c => c.Long());
            CreateIndex("dbo.Request", "TourId");
            AddForeignKey("dbo.Request", "TourId", "dbo.Tour", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "TourId", "dbo.Tour");
            DropIndex("dbo.Request", new[] { "TourId" });
            DropColumn("dbo.Request", "TourId");
        }
    }
}
