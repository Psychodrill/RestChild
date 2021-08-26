namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MPGUIntegrationMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MGTVisitBooking", newName: "MGTBookingVisit");
            DropForeignKey("dbo.ScheduleMessage", "FK_dbo.ScheduleMessage_dbo.ScheduleBookingMosgortur_ScheduleBookingMosgorturId");
            //DropForeignKey("dbo.ScheduleMessage", "MGTVisitBookingId", "dbo.MGTVisitBooking");
            DropIndex("dbo.ScheduleMessage", new[] { "MGTVisitBookingId" });
            CreateTable(
                "dbo.MGTVisitBookingStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        MGTCode = c.String(maxLength: 1000),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(maxLength: 1000),
                        ParrentId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MGTVisitBookingStatus", t => t.ParrentId)
                .Index(t => t.ParrentId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MGTVisitBookingMPGUStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Parent = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MGTVisitBookingMPGUStatusModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        StatusFromId = c.Long(),
                        StatusToId = c.Long(),
                        MPGUStatusId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MGTVisitBookingMPGUStatus", t => t.MPGUStatusId)
                .ForeignKey("dbo.MGTVisitBookingStatus", t => t.StatusFromId)
                .ForeignKey("dbo.MGTVisitBookingStatus", t => t.StatusToId)
                .Index(t => t.StatusFromId)
                .Index(t => t.StatusToId)
                .Index(t => t.MPGUStatusId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.MGTVisitTarget", "SlotsCount", c => c.Int());
            AddColumn("dbo.MGTVisitTarget", "Description", c => c.String());
            AddColumn("dbo.MGTBookingVisit", "VisitCell", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.MGTBookingVisit", "PINCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "MPGURegNum", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "ServiceNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "MPGURegDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.MGTBookingVisit", "StatusId", c => c.Long());
            AddColumn("dbo.MGTBookingVisit", "TargetId", c => c.Long());
            AddColumn("dbo.MGTBookingVisit", "ParrentId", c => c.Long());
            AlterColumn("dbo.MGTVisitTarget", "Name", c => c.String(nullable: false, maxLength: 1000));
            CreateIndex("dbo.MGTBookingVisit", "StatusId");
            CreateIndex("dbo.MGTBookingVisit", "TargetId");
            CreateIndex("dbo.MGTBookingVisit", "ParrentId");
            AddForeignKey("dbo.MGTBookingVisit", "ParrentId", "dbo.MGTBookingVisit", "Id");
            AddForeignKey("dbo.MGTBookingVisit", "StatusId", "dbo.MGTVisitBookingStatus", "Id");
            AddForeignKey("dbo.MGTBookingVisit", "TargetId", "dbo.MGTVisitTarget", "Id");
            DropColumn("dbo.ScheduleMessage", "MGTVisitBookingId");
            DropColumn("dbo.MGTBookingVisit", "DateShedule");
            DropColumn("dbo.MGTBookingVisit", "Number");
            DropColumn("dbo.MGTBookingVisit", "Office");
            DropColumn("dbo.MGTBookingVisit", "Target");
            DropColumn("dbo.MGTBookingVisit", "TypeBooking");
            DropColumn("dbo.MGTBookingVisit", "Title");
            DropColumn("dbo.MGTBookingVisit", "Canceled");
            DropColumn("dbo.MGTBookingVisit", "BookingCode");
            DropColumn("dbo.MGTBookingVisit", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MGTBookingVisit", "Address", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "BookingCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "Canceled", c => c.Boolean(nullable: false));
            AddColumn("dbo.MGTBookingVisit", "Title", c => c.String());
            AddColumn("dbo.MGTBookingVisit", "TypeBooking", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "Target", c => c.String());
            AddColumn("dbo.MGTBookingVisit", "Office", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "Number", c => c.String(maxLength: 1000));
            AddColumn("dbo.MGTBookingVisit", "DateShedule", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ScheduleMessage", "MGTVisitBookingId", c => c.Long());
            DropForeignKey("dbo.MGTVisitBookingMPGUStatusModel", "StatusToId", "dbo.MGTVisitBookingStatus");
            DropForeignKey("dbo.MGTVisitBookingMPGUStatusModel", "StatusFromId", "dbo.MGTVisitBookingStatus");
            DropForeignKey("dbo.MGTVisitBookingMPGUStatusModel", "MPGUStatusId", "dbo.MGTVisitBookingMPGUStatus");
            DropForeignKey("dbo.MGTBookingVisit", "TargetId", "dbo.MGTVisitTarget");
            DropForeignKey("dbo.MGTBookingVisit", "StatusId", "dbo.MGTVisitBookingStatus");
            DropForeignKey("dbo.MGTVisitBookingStatus", "ParrentId", "dbo.MGTVisitBookingStatus");
            DropForeignKey("dbo.MGTBookingVisit", "ParrentId", "dbo.MGTBookingVisit");
            DropIndex("dbo.MGTVisitBookingMPGUStatusModel", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTVisitBookingMPGUStatusModel", new[] { "Eid" });
            DropIndex("dbo.MGTVisitBookingMPGUStatusModel", new[] { "MPGUStatusId" });
            DropIndex("dbo.MGTVisitBookingMPGUStatusModel", new[] { "StatusToId" });
            DropIndex("dbo.MGTVisitBookingMPGUStatusModel", new[] { "StatusFromId" });
            DropIndex("dbo.MGTVisitBookingMPGUStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTVisitBookingMPGUStatus", new[] { "Eid" });
            DropIndex("dbo.MGTVisitBookingStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.MGTVisitBookingStatus", new[] { "Eid" });
            DropIndex("dbo.MGTVisitBookingStatus", new[] { "ParrentId" });
            DropIndex("dbo.MGTBookingVisit", new[] { "ParrentId" });
            DropIndex("dbo.MGTBookingVisit", new[] { "TargetId" });
            DropIndex("dbo.MGTBookingVisit", new[] { "StatusId" });
            AlterColumn("dbo.MGTVisitTarget", "Name", c => c.String(nullable: false));
            DropColumn("dbo.MGTBookingVisit", "ParrentId");
            DropColumn("dbo.MGTBookingVisit", "TargetId");
            DropColumn("dbo.MGTBookingVisit", "StatusId");
            DropColumn("dbo.MGTBookingVisit", "MPGURegDate");
            DropColumn("dbo.MGTBookingVisit", "ServiceNumber");
            DropColumn("dbo.MGTBookingVisit", "MPGURegNum");
            DropColumn("dbo.MGTBookingVisit", "PINCode");
            DropColumn("dbo.MGTBookingVisit", "VisitCell");
            DropColumn("dbo.MGTVisitTarget", "Description");
            DropColumn("dbo.MGTVisitTarget", "SlotsCount");
            DropTable("dbo.MGTVisitBookingMPGUStatusModel");
            DropTable("dbo.MGTVisitBookingMPGUStatus");
            DropTable("dbo.MGTVisitBookingStatus");
            CreateIndex("dbo.ScheduleMessage", "MGTVisitBookingId");
            //AddForeignKey("dbo.ScheduleMessage", "MGTVisitBookingId", "dbo.MGTVisitBooking", "Id");
            RenameTable(name: "dbo.MGTBookingVisit", newName: "MGTVisitBooking");
            AddForeignKey("dbo.ScheduleMessage", "MGTVisitBookingId", "dbo.MGTVisitBooking", "Id", false, "FK_dbo.ScheduleMessage_dbo.ScheduleBookingMosgortur_ScheduleBookingMosgorturId");
        }
    }
}
