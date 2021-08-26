namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BIAndScheduleBookingMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountHistoryLogin",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateEnter = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateLastActivity = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateExit = c.DateTime(precision: 7, storeType: "datetime2"),
                        SessionUid = c.String(maxLength: 1000),
                        RemoteAddr = c.String(maxLength: 1000),
                        UserAgent = c.String(),
                        StopSession = c.Boolean(nullable: false),
                        Login = c.String(maxLength: 1000),
                        IsAuthorized = c.Boolean(nullable: false),
                        AccountId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.ScheduleBookingMosgortur",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateShedule = c.DateTime(precision: 7, storeType: "datetime2"),
                        Number = c.String(maxLength: 1000),
                        Office = c.String(maxLength: 1000),
                        Target = c.String(),
                        TypeBooking = c.String(maxLength: 1000),
                        Title = c.String(),
                        Canceled = c.Boolean(nullable: false),
                        BookingCode = c.String(maxLength: 1000),
                        Address = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.ScheduleBookingMosgorturPerson",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypePerson = c.String(maxLength: 1000),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        Male = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        Phone = c.String(maxLength: 1000),
                        Snils = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        Benefit = c.String(maxLength: 1000),
                        SheduleId = c.Long(),
                        PersonTypeId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduleBookingMosgorturPersonType", t => t.PersonTypeId)
                .ForeignKey("dbo.ScheduleBookingMosgortur", t => t.SheduleId)
                .Index(t => t.SheduleId)
                .Index(t => t.PersonTypeId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
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
            
            CreateTable(
                "dbo.ScheduleMessage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(),
                        DateMessage = c.DateTime(precision: 7, storeType: "datetime2"),
                        Processed = c.Boolean(nullable: false),
                        HasError = c.Boolean(nullable: false),
                        ErrorMessage = c.String(),
                        ScheduleBookingMosgorturId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduleBookingMosgortur", t => t.ScheduleBookingMosgorturId)
                .Index(t => t.ScheduleBookingMosgorturId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.SecurityJournal",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateEvent = c.DateTime(precision: 7, storeType: "datetime2"),
                        EventName = c.String(maxLength: 1000),
                        Description = c.String(),
                        Brouser = c.String(maxLength: 1000),
                        UserName = c.String(maxLength: 1000),
                        SecurityJournalTypeId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SecurityJournalType", t => t.SecurityJournalTypeId)
                .Index(t => t.SecurityJournalTypeId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.SecurityJournalType",
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
            
            CreateTable(
                "dbo.SecuritySetting",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        ValueJson = c.String(),
                        ValueInt = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecurityJournal", "SecurityJournalTypeId", "dbo.SecurityJournalType");
            DropForeignKey("dbo.ScheduleMessage", "ScheduleBookingMosgorturId", "dbo.ScheduleBookingMosgortur");
            DropForeignKey("dbo.ScheduleBookingMosgorturPerson", "SheduleId", "dbo.ScheduleBookingMosgortur");
            DropForeignKey("dbo.ScheduleBookingMosgorturPerson", "PersonTypeId", "dbo.ScheduleBookingMosgorturPersonType");
            DropForeignKey("dbo.AccountHistoryLogin", "AccountId", "dbo.Account");
            DropIndex("dbo.SecuritySetting", new[] { "EidSendStatus" });
            DropIndex("dbo.SecuritySetting", new[] { "Eid" });
            DropIndex("dbo.SecurityJournalType", new[] { "EidSendStatus" });
            DropIndex("dbo.SecurityJournalType", new[] { "Eid" });
            DropIndex("dbo.SecurityJournal", new[] { "EidSendStatus" });
            DropIndex("dbo.SecurityJournal", new[] { "Eid" });
            DropIndex("dbo.SecurityJournal", new[] { "SecurityJournalTypeId" });
            DropIndex("dbo.ScheduleMessage", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleMessage", new[] { "Eid" });
            DropIndex("dbo.ScheduleMessage", new[] { "ScheduleBookingMosgorturId" });
            DropIndex("dbo.ScheduleBookingMosgorturStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleBookingMosgorturStatus", new[] { "Eid" });
            DropIndex("dbo.ScheduleBookingMosgorturPersonType", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleBookingMosgorturPersonType", new[] { "Eid" });
            DropIndex("dbo.ScheduleBookingMosgorturPerson", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleBookingMosgorturPerson", new[] { "Eid" });
            DropIndex("dbo.ScheduleBookingMosgorturPerson", new[] { "PersonTypeId" });
            DropIndex("dbo.ScheduleBookingMosgorturPerson", new[] { "SheduleId" });
            DropIndex("dbo.ScheduleBookingMosgortur", new[] { "EidSendStatus" });
            DropIndex("dbo.ScheduleBookingMosgortur", new[] { "Eid" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "EidSendStatus" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "Eid" });
            DropIndex("dbo.AccountHistoryLogin", new[] { "AccountId" });
            DropTable("dbo.SecuritySetting");
            DropTable("dbo.SecurityJournalType");
            DropTable("dbo.SecurityJournal");
            DropTable("dbo.ScheduleMessage");
            DropTable("dbo.ScheduleBookingMosgorturStatus");
            DropTable("dbo.ScheduleBookingMosgorturPersonType");
            DropTable("dbo.ScheduleBookingMosgorturPerson");
            DropTable("dbo.ScheduleBookingMosgortur");
            DropTable("dbo.AccountHistoryLogin");
        }
    }
}
