namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingMosgorturOnlineQueueMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ScheduleBookingMosgortur", newName: "MGTVisitBooking");
            RenameTable(name: "dbo.ScheduleBookingMosgorturPerson", newName: "MGTVisitBookingPerson");
            RenameTable(name: "dbo.ScheduleBookingMosgorturPersonType", newName: "MGTVisitBookingPersonType");
            RenameTable(name: "dbo.ScheduleBookingMosgorturStatus", newName: "MGTVisitTarget");
            DropForeignKey("dbo.MGTVisitTargetMGTWorkingDay", "MGTVisitTarget_Id", "dbo.MGTVisitTarget");
            RenameColumn(table: "dbo.MGTVisitBookingPerson", name: "SheduleId", newName: "VisitBookingId");
            RenameColumn(table: "dbo.ScheduleMessage", name: "ScheduleBookingMosgorturId", newName: "MGTVisitBookingId");
            RenameIndex(table: "dbo.MGTVisitBookingPerson", name: "IX_SheduleId", newName: "IX_VisitBookingId");
            RenameIndex(table: "dbo.ScheduleMessage", name: "IX_ScheduleBookingMosgorturId", newName: "IX_MGTVisitBookingId");
            DropPrimaryKey("dbo.MGTVisitTarget");
            CreateTable(
                "dbo.MGTWorkingDaysHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 1000),
                        EventDescription = c.String(),
                        EventDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        WorkingDayId = c.Long(),
                        AuthorId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MGTWorkingDay", t => t.WorkingDayId)
                .ForeignKey("dbo.Account", t => t.AuthorId)
                .Index(t => t.WorkingDayId)
                .Index(t => t.AuthorId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MGTWorkingDay",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        WorkingInterval = c.Short(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MGTWorkingDayWindow",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsCanceled = c.Boolean(nullable: false),
                        WorkingDayId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MGTWorkingDay", t => t.WorkingDayId)
                .Index(t => t.WorkingDayId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MGTWindowWorkingPeriod",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TimeFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TimeTo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        WindowId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MGTWorkingDayWindow", t => t.WindowId)
                .Index(t => t.WindowId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MGTVisitTargetMGTWorkingDay",
                c => new
                    {
                        MGTVisitTarget_Id = c.Long(nullable: false),
                        MGTWorkingDay_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.MGTVisitTarget_Id, t.MGTWorkingDay_Id })
                .ForeignKey("dbo.MGTVisitTarget", t => t.MGTVisitTarget_Id, cascadeDelete: true)
                .ForeignKey("dbo.MGTWorkingDay", t => t.MGTWorkingDay_Id, cascadeDelete: true)
                .Index(t => t.MGTVisitTarget_Id)
                .Index(t => t.MGTWorkingDay_Id);
            
            AddColumn("dbo.MGTVisitBooking", "WorkingDayId", c => c.Long());
            AddColumn("dbo.MGTVisitTarget", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MGTVisitTarget", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.MGTVisitTarget", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.MGTVisitTarget", "Id");
            CreateIndex("dbo.MGTVisitBooking", "WorkingDayId");
            AddForeignKey("dbo.MGTVisitBooking", "WorkingDayId", "dbo.MGTWorkingDay", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MGTWorkingDaysHistory", "AuthorId", "dbo.Account");
            DropForeignKey("dbo.MGTWorkingDaysHistory", "WorkingDayId", "dbo.MGTWorkingDay");
            DropForeignKey("dbo.MGTWorkingDayWindow", "WorkingDayId", "dbo.MGTWorkingDay");
            DropForeignKey("dbo.MGTWindowWorkingPeriod", "WindowId", "dbo.MGTWorkingDayWindow");
            DropForeignKey("dbo.MGTVisitBooking", "WorkingDayId", "dbo.MGTWorkingDay");
            DropForeignKey("dbo.MGTVisitTargetMGTWorkingDay", "MGTWorkingDay_Id", "dbo.MGTWorkingDay");
            DropForeignKey("dbo.MGTVisitTargetMGTWorkingDay", "MGTVisitTarget_Id", "dbo.MGTVisitTarget");
            DropIndex("dbo.MGTVisitTargetMGTWorkingDay", new[] { "MGTWorkingDay_Id" });
            DropIndex("dbo.MGTVisitTargetMGTWorkingDay", new[] { "MGTVisitTarget_Id" });
            DropIndex("dbo.MGTWindowWorkingPeriod", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTWindowWorkingPeriod", new[] { "Eid" });
            DropIndex("dbo.MGTWindowWorkingPeriod", new[] { "WindowId" });
            DropIndex("dbo.MGTWorkingDayWindow", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTWorkingDayWindow", new[] { "Eid" });
            DropIndex("dbo.MGTWorkingDayWindow", new[] { "WorkingDayId" });
            DropIndex("dbo.MGTVisitBooking", new[] { "WorkingDayId" });
            DropIndex("dbo.MGTWorkingDay", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTWorkingDay", new[] { "Eid" });
            DropIndex("dbo.MGTWorkingDaysHistory", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTWorkingDaysHistory", new[] { "Eid" });
            DropIndex("dbo.MGTWorkingDaysHistory", new[] { "AuthorId" });
            DropIndex("dbo.MGTWorkingDaysHistory", new[] { "WorkingDayId" });
            DropPrimaryKey("dbo.MGTVisitTarget");
            AlterColumn("dbo.MGTVisitTarget", "Name", c => c.String(maxLength: 1000));
            AlterColumn("dbo.MGTVisitTarget", "Id", c => c.Long(nullable: false));
            DropColumn("dbo.MGTVisitTarget", "IsActive");
            DropColumn("dbo.MGTVisitBooking", "WorkingDayId");
            DropTable("dbo.MGTVisitTargetMGTWorkingDay");
            DropTable("dbo.MGTWindowWorkingPeriod");
            DropTable("dbo.MGTWorkingDayWindow");
            DropTable("dbo.MGTWorkingDay");
            DropTable("dbo.MGTWorkingDaysHistory");
            AddPrimaryKey("dbo.MGTVisitTarget", "Id");
            RenameIndex(table: "dbo.ScheduleMessage", name: "IX_MGTVisitBookingId", newName: "IX_ScheduleBookingMosgorturId");
            RenameIndex(table: "dbo.MGTVisitBookingPerson", name: "IX_VisitBookingId", newName: "IX_SheduleId");
            RenameColumn(table: "dbo.ScheduleMessage", name: "MGTVisitBookingId", newName: "ScheduleBookingMosgorturId");
            RenameColumn(table: "dbo.MGTVisitBookingPerson", name: "VisitBookingId", newName: "SheduleId");
            AddForeignKey("dbo.MGTVisitTargetMGTWorkingDay", "MGTVisitTarget_Id", "dbo.MGTVisitTarget", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.MGTVisitTarget", newName: "ScheduleBookingMosgorturStatus");
            RenameTable(name: "dbo.MGTVisitBookingPersonType", newName: "ScheduleBookingMosgorturPersonType");
            RenameTable(name: "dbo.MGTVisitBookingPerson", newName: "ScheduleBookingMosgorturPerson");
            RenameTable(name: "dbo.MGTVisitBooking", newName: "ScheduleBookingMosgortur");
        }
    }
}
