namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingMigrationTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.Guid(nullable: false),
                        BookingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CountRooms = c.Int(),
                        CountPlace = c.Int(),
                        CountAttendants = c.Int(),
                        Canceled = c.Boolean(nullable: false),
                        RequestId = c.Long(),
                        TourVolumeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.TourVolume", t => t.TourVolumeId)
                .Index(t => t.RequestId)
                .Index(t => t.TourVolumeId);
            
            AddColumn("dbo.Applicant", "TourVolumeId", c => c.Long());
            AddColumn("dbo.Child", "TourVolumeId", c => c.Long());
            AddColumn("dbo.Request", "HotelsId", c => c.Long());
            AddColumn("dbo.TourVolume", "CountRooms", c => c.Int());
            AddColumn("dbo.TourVolume", "CountBusyRooms", c => c.Int());
            AddColumn("dbo.TourVolume", "CountPlace", c => c.Int());
            AddColumn("dbo.TourVolume", "CountBusyPlace", c => c.Int());
            AddColumn("dbo.TypeOfRooms", "CountBasePlace", c => c.Int(nullable: false));
            AddColumn("dbo.TypeOfRooms", "CountAddonPlace", c => c.Int(nullable: false));
            CreateIndex("dbo.Applicant", "TourVolumeId");
            CreateIndex("dbo.Child", "TourVolumeId");
            CreateIndex("dbo.Request", "HotelsId");
            AddForeignKey("dbo.Request", "HotelsId", "dbo.Hotels", "Id");
            AddForeignKey("dbo.Child", "TourVolumeId", "dbo.TourVolume", "Id");
            AddForeignKey("dbo.Applicant", "TourVolumeId", "dbo.TourVolume", "Id");
            DropColumn("dbo.TourVolume", "CountBasePlace");
            DropColumn("dbo.TourVolume", "CountAddonPlace");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourVolume", "CountAddonPlace", c => c.Int(nullable: false));
            AddColumn("dbo.TourVolume", "CountBasePlace", c => c.Int(nullable: false));
            DropForeignKey("dbo.Booking", "TourVolumeId", "dbo.TourVolume");
            DropForeignKey("dbo.Booking", "RequestId", "dbo.Request");
            DropForeignKey("dbo.Applicant", "TourVolumeId", "dbo.TourVolume");
            DropForeignKey("dbo.Child", "TourVolumeId", "dbo.TourVolume");
            DropForeignKey("dbo.Request", "HotelsId", "dbo.Hotels");
            DropIndex("dbo.Booking", new[] { "TourVolumeId" });
            DropIndex("dbo.Booking", new[] { "RequestId" });
            DropIndex("dbo.Request", new[] { "HotelsId" });
            DropIndex("dbo.Child", new[] { "TourVolumeId" });
            DropIndex("dbo.Applicant", new[] { "TourVolumeId" });
            DropColumn("dbo.TypeOfRooms", "CountAddonPlace");
            DropColumn("dbo.TypeOfRooms", "CountBasePlace");
            DropColumn("dbo.TourVolume", "CountBusyPlace");
            DropColumn("dbo.TourVolume", "CountPlace");
            DropColumn("dbo.TourVolume", "CountBusyRooms");
            DropColumn("dbo.TourVolume", "CountRooms");
            DropColumn("dbo.Request", "HotelsId");
            DropColumn("dbo.Child", "TourVolumeId");
            DropColumn("dbo.Applicant", "TourVolumeId");
            DropTable("dbo.Booking");
        }
    }
}
