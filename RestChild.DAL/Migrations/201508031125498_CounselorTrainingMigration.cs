namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CounselorTrainingMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectOfRestFile", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropIndex("dbo.SubjectOfRestFile", new[] { "SubjectOfRestId" });
            CreateTable(
                "dbo.TrainingCounselorsResult",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Comment = c.String(),
                        Rank = c.Int(),
                        DateOfFinalTest = c.DateTime(precision: 7, storeType: "datetime2"),
                        CounselorsId = c.Long(),
                        AdministratorTourId = c.Long(),
                        TrainingCounselorsId = c.Long(),
                        StatusId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .ForeignKey("dbo.TrainingCounselorsResultStatus", t => t.StatusId)
                .ForeignKey("dbo.TrainingCounselors", t => t.TrainingCounselorsId)
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTourId)
                .Index(t => t.CounselorsId)
                .Index(t => t.AdministratorTourId)
                .Index(t => t.TrainingCounselorsId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.LinkToFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HandlerHelper = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileOrLink",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        IsPhoto = c.Boolean(nullable: false),
                        IsVideo = c.Boolean(nullable: false),
                        LinkId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LinkToFile", t => t.LinkId)
                .Index(t => t.LinkId);
            
            CreateTable(
                "dbo.CounselorsStopListReason",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingCounselorsResultStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingCounselors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(),
                        Value = c.Int(),
                        StartLeanring = c.DateTime(precision: 7, storeType: "datetime2"),
                        Timetable = c.String(),
                        TrainingCounselorsTypeId = c.Long(),
                        TrainingCounselorsTimeId = c.Long(),
                        LinkToFileId = c.Long(),
                        HistoryLinkId = c.Long(),
                        TrainingCounselorsPlaceId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.LinkToFile", t => t.LinkToFileId)
                .ForeignKey("dbo.TrainingCounselorsPlace", t => t.TrainingCounselorsPlaceId)
                .ForeignKey("dbo.TrainingCounselorsTime", t => t.TrainingCounselorsTimeId)
                .ForeignKey("dbo.TrainingCounselorsType", t => t.TrainingCounselorsTypeId)
                .Index(t => t.TrainingCounselorsTypeId)
                .Index(t => t.TrainingCounselorsTimeId)
                .Index(t => t.LinkToFileId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.TrainingCounselorsPlaceId);
            
            CreateTable(
                "dbo.TrainingCounselorsPlace",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        AddressId = c.Long(),
                        LinkToFileId = c.Long(),
                        HistoryLinkId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.LinkToFile", t => t.LinkToFileId)
                .Index(t => t.AddressId)
                .Index(t => t.LinkToFileId)
                .Index(t => t.HistoryLinkId);
            
            CreateTable(
                "dbo.TrainingCounselorsTime",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingCounselorsType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoutAttendant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        HaveMiddleName = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        AdministratorTourId = c.Long(),
                        CounselorsId = c.Long(),
                        BoutId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTourId)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .ForeignKey("dbo.Bout", t => t.BoutId)
                .Index(t => t.AdministratorTourId)
                .Index(t => t.CounselorsId)
                .Index(t => t.BoutId);
            
            AddColumn("dbo.Bout", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.AdministratorTour", "StopListReasonText", c => c.String());
            AddColumn("dbo.AdministratorTour", "StopListReasonId", c => c.Long());
            AddColumn("dbo.Counselors", "StopListReasonText", c => c.String());
            AddColumn("dbo.Counselors", "StopListReasonId", c => c.Long());
            AddColumn("dbo.CouncelorComment", "Rank", c => c.Int());
            AddColumn("dbo.CouncelorComment", "Answer", c => c.String());
            AddColumn("dbo.CouncelorComment", "AnswerDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CouncelorComment", "Checked", c => c.Boolean(nullable: false));
            AddColumn("dbo.CouncelorComment", "AnswerAccountId", c => c.Long());
            AddColumn("dbo.SubjectOfRest", "LinkToFileId", c => c.Long());
            CreateIndex("dbo.Bout", "HistoryLinkId");
            CreateIndex("dbo.AdministratorTour", "StopListReasonId");
            CreateIndex("dbo.Counselors", "StopListReasonId");
            CreateIndex("dbo.CouncelorComment", "AnswerAccountId");
            CreateIndex("dbo.SubjectOfRest", "LinkToFileId");
            AddForeignKey("dbo.CouncelorComment", "AnswerAccountId", "dbo.Account", "Id");
            AddForeignKey("dbo.SubjectOfRest", "LinkToFileId", "dbo.LinkToFile", "Id");
            AddForeignKey("dbo.Counselors", "StopListReasonId", "dbo.CounselorsStopListReason", "Id");
            AddForeignKey("dbo.AdministratorTour", "StopListReasonId", "dbo.CounselorsStopListReason", "Id");
            AddForeignKey("dbo.Bout", "HistoryLinkId", "dbo.HistoryLink", "Id");
            DropTable("dbo.SubjectOfRestFile");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubjectOfRestFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileUrl = c.String(nullable: false, maxLength: 1000),
                        IsPhoto = c.Boolean(nullable: false),
                        IsVideo = c.Boolean(nullable: false),
                        SubjectOfRestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Bout", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.BoutAttendant", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.BoutAttendant", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.BoutAttendant", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.AdministratorTour", "StopListReasonId", "dbo.CounselorsStopListReason");
            DropForeignKey("dbo.TrainingCounselorsResult", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.TrainingCounselors", "TrainingCounselorsTypeId", "dbo.TrainingCounselorsType");
            DropForeignKey("dbo.TrainingCounselors", "TrainingCounselorsTimeId", "dbo.TrainingCounselorsTime");
            DropForeignKey("dbo.TrainingCounselors", "TrainingCounselorsPlaceId", "dbo.TrainingCounselorsPlace");
            DropForeignKey("dbo.TrainingCounselorsPlace", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.TrainingCounselorsPlace", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.TrainingCounselorsPlace", "AddressId", "dbo.Address");
            DropForeignKey("dbo.TrainingCounselorsResult", "TrainingCounselorsId", "dbo.TrainingCounselors");
            DropForeignKey("dbo.TrainingCounselors", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.TrainingCounselors", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.TrainingCounselorsResult", "StatusId", "dbo.TrainingCounselorsResultStatus");
            DropForeignKey("dbo.Counselors", "StopListReasonId", "dbo.CounselorsStopListReason");
            DropForeignKey("dbo.TrainingCounselorsResult", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.SubjectOfRest", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.FileOrLink", "LinkId", "dbo.LinkToFile");
            DropForeignKey("dbo.CouncelorComment", "AnswerAccountId", "dbo.Account");
            DropIndex("dbo.BoutAttendant", new[] { "BoutId" });
            DropIndex("dbo.BoutAttendant", new[] { "CounselorsId" });
            DropIndex("dbo.BoutAttendant", new[] { "AdministratorTourId" });
            DropIndex("dbo.TrainingCounselorsPlace", new[] { "HistoryLinkId" });
            DropIndex("dbo.TrainingCounselorsPlace", new[] { "LinkToFileId" });
            DropIndex("dbo.TrainingCounselorsPlace", new[] { "AddressId" });
            DropIndex("dbo.TrainingCounselors", new[] { "TrainingCounselorsPlaceId" });
            DropIndex("dbo.TrainingCounselors", new[] { "HistoryLinkId" });
            DropIndex("dbo.TrainingCounselors", new[] { "LinkToFileId" });
            DropIndex("dbo.TrainingCounselors", new[] { "TrainingCounselorsTimeId" });
            DropIndex("dbo.TrainingCounselors", new[] { "TrainingCounselorsTypeId" });
            DropIndex("dbo.FileOrLink", new[] { "LinkId" });
            DropIndex("dbo.SubjectOfRest", new[] { "LinkToFileId" });
            DropIndex("dbo.CouncelorComment", new[] { "AnswerAccountId" });
            DropIndex("dbo.Counselors", new[] { "StopListReasonId" });
            DropIndex("dbo.TrainingCounselorsResult", new[] { "StatusId" });
            DropIndex("dbo.TrainingCounselorsResult", new[] { "TrainingCounselorsId" });
            DropIndex("dbo.TrainingCounselorsResult", new[] { "AdministratorTourId" });
            DropIndex("dbo.TrainingCounselorsResult", new[] { "CounselorsId" });
            DropIndex("dbo.AdministratorTour", new[] { "StopListReasonId" });
            DropIndex("dbo.Bout", new[] { "HistoryLinkId" });
            DropColumn("dbo.SubjectOfRest", "LinkToFileId");
            DropColumn("dbo.CouncelorComment", "AnswerAccountId");
            DropColumn("dbo.CouncelorComment", "Checked");
            DropColumn("dbo.CouncelorComment", "AnswerDate");
            DropColumn("dbo.CouncelorComment", "Answer");
            DropColumn("dbo.CouncelorComment", "Rank");
            DropColumn("dbo.Counselors", "StopListReasonId");
            DropColumn("dbo.Counselors", "StopListReasonText");
            DropColumn("dbo.AdministratorTour", "StopListReasonId");
            DropColumn("dbo.AdministratorTour", "StopListReasonText");
            DropColumn("dbo.Bout", "HistoryLinkId");
            DropTable("dbo.BoutAttendant");
            DropTable("dbo.TrainingCounselorsType");
            DropTable("dbo.TrainingCounselorsTime");
            DropTable("dbo.TrainingCounselorsPlace");
            DropTable("dbo.TrainingCounselors");
            DropTable("dbo.TrainingCounselorsResultStatus");
            DropTable("dbo.CounselorsStopListReason");
            DropTable("dbo.FileOrLink");
            DropTable("dbo.LinkToFile");
            DropTable("dbo.TrainingCounselorsResult");
            CreateIndex("dbo.SubjectOfRestFile", "SubjectOfRestId");
            AddForeignKey("dbo.SubjectOfRestFile", "SubjectOfRestId", "dbo.SubjectOfRest", "Id");
        }
    }
}
