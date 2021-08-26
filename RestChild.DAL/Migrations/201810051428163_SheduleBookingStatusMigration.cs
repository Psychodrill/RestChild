namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SheduleBookingStatusMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleBookingMosgorturPersonType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Code = c.String(maxLength: 1000),
                        Name = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.ScheduleBookingMosgorturStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.ScheduleBookingMosgortur", "BookingCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.ScheduleBookingMosgorturPerson", "PersonTypeId", c => c.Long());
            CreateIndex("dbo.ScheduleBookingMosgorturPerson", "PersonTypeId");
            AddForeignKey("dbo.ScheduleBookingMosgorturPerson", "PersonTypeId", "dbo.ScheduleBookingMosgorturPersonType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleBookingMosgorturPerson", "PersonTypeId", "dbo.ScheduleBookingMosgorturPersonType");
            DropIndex("dbo.ScheduleBookingMosgorturStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleBookingMosgorturStatus", new[] { "Eid" });
            DropIndex("dbo.ScheduleBookingMosgorturPersonType", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleBookingMosgorturPersonType", new[] { "Eid" });
            DropIndex("dbo.ScheduleBookingMosgorturPerson", new[] { "PersonTypeId" });
            DropColumn("dbo.ScheduleBookingMosgorturPerson", "PersonTypeId");
            DropColumn("dbo.ScheduleBookingMosgortur", "BookingCode");
            DropTable("dbo.ScheduleBookingMosgorturStatus");
            DropTable("dbo.ScheduleBookingMosgorturPersonType");
        }
    }
}
