namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingCommercialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingCommercial",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateBooking = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uid = c.Guid(nullable: false),
                        IsCancel = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Count = c.Int(nullable: false),
                        RequestId = c.Long(),
                        TourVolumeId = c.Long(),
                        TourId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .ForeignKey("dbo.TourVolume", t => t.TourVolumeId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.RequestId)
                .Index(t => t.TourVolumeId)
                .Index(t => t.TourId);
            
            AddColumn("dbo.TourVolume", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingCommercial", "RequestId", "dbo.Request");
            DropForeignKey("dbo.BookingCommercial", "TourVolumeId", "dbo.TourVolume");
            DropForeignKey("dbo.BookingCommercial", "TourId", "dbo.Tour");
            DropIndex("dbo.BookingCommercial", new[] { "TourId" });
            DropIndex("dbo.BookingCommercial", new[] { "TourVolumeId" });
            DropIndex("dbo.BookingCommercial", new[] { "RequestId" });
            DropColumn("dbo.TourVolume", "RowVersion");
            DropTable("dbo.BookingCommercial");
        }
    }
}
