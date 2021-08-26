namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcelarusTaskMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Counselors", new[] { "CounselorCourcesId" });
            RenameColumn(table: "dbo.CouncelorComment", name: "CommentsId", newName: "CounselorsId");
            RenameColumn(table: "dbo.CounselorHighSchool", name: "HighSchoolGraduationsId", newName: "CounselorsId");
            RenameIndex(table: "dbo.CouncelorComment", name: "IX_CommentsId", newName: "IX_CounselorsId");
            RenameIndex(table: "dbo.CounselorHighSchool", name: "IX_HighSchoolGraduationsId", newName: "IX_CounselorsId");
            CreateTable(
                "dbo.CounselorCource",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Year = c.Int(nullable: false),
                        CounselorsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CounselorsId);
            
            CreateTable(
                "dbo.CounselorTask",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateCreate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DatePlanFinish = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateFactFinish = c.DateTime(precision: 7, storeType: "datetime2"),
                        Subject = c.String(),
                        Body = c.String(),
                        Report = c.String(),
                        BaseTask = c.Boolean(nullable: false),
                        AdministratorTourId = c.Long(),
                        CounselorsId = c.Long(),
                        BoutId = c.Long(),
                        HistoryLinkId = c.Long(),
                        PartyId = c.Long(),
                        AccountId = c.Long(),
                        ParentId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTourId)
                .ForeignKey("dbo.Bout", t => t.BoutId)
                .ForeignKey("dbo.CounselorTask", t => t.ParentId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Party", t => t.PartyId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .Index(t => t.AdministratorTourId)
                .Index(t => t.CounselorsId)
                .Index(t => t.BoutId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.PartyId)
                .Index(t => t.AccountId)
                .Index(t => t.ParentId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.CounselorTaskFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileTitle = c.String(nullable: false, maxLength: 1000),
                        DataCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CounselorTaskId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounselorTask", t => t.CounselorTaskId)
                .Index(t => t.CounselorTaskId);
            
            CreateTable(
                "dbo.CounselorTaskReportFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 1000),
                        FileTitle = c.String(nullable: false, maxLength: 1000),
                        DataCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CounselorTaskId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounselorTask", t => t.CounselorTaskId)
                .Index(t => t.CounselorTaskId);

			DropForeignKey("dbo.Counselors", "CounselorCourcesId", "dbo.CounselorCources");
			DropColumn("dbo.Counselors", "CounselorCourcesId");
            DropTable("dbo.CounselorCources");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CounselorCources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Counselors", "CounselorCourcesId", c => c.Long());
            DropForeignKey("dbo.CounselorTask", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.CounselorTask", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.CounselorTaskReportFile", "CounselorTaskId", "dbo.CounselorTask");
            DropForeignKey("dbo.CounselorTask", "PartyId", "dbo.Party");
            DropForeignKey("dbo.CounselorTask", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.CounselorTaskFile", "CounselorTaskId", "dbo.CounselorTask");
            DropForeignKey("dbo.CounselorTask", "ParentId", "dbo.CounselorTask");
            DropForeignKey("dbo.CounselorTask", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.CounselorTask", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.CounselorTask", "AccountId", "dbo.Account");
            DropIndex("dbo.CounselorTaskReportFile", new[] { "CounselorTaskId" });
            DropIndex("dbo.CounselorTaskFile", new[] { "CounselorTaskId" });
            DropIndex("dbo.CounselorTask", new[] { "StateId" });
            DropIndex("dbo.CounselorTask", new[] { "ParentId" });
            DropIndex("dbo.CounselorTask", new[] { "AccountId" });
            DropIndex("dbo.CounselorTask", new[] { "PartyId" });
            DropIndex("dbo.CounselorTask", new[] { "HistoryLinkId" });
            DropIndex("dbo.CounselorTask", new[] { "BoutId" });
            DropIndex("dbo.CounselorTask", new[] { "CounselorsId" });
            DropIndex("dbo.CounselorTask", new[] { "AdministratorTourId" });
            DropIndex("dbo.CounselorCource", new[] { "CounselorsId" });
            DropTable("dbo.CounselorTaskReportFile");
            DropTable("dbo.CounselorTaskFile");
            DropTable("dbo.CounselorTask");
            DropTable("dbo.CounselorCource");
            RenameIndex(table: "dbo.CounselorHighSchool", name: "IX_CounselorsId", newName: "IX_HighSchoolGraduationsId");
            RenameIndex(table: "dbo.CouncelorComment", name: "IX_CounselorsId", newName: "IX_CommentsId");
            RenameColumn(table: "dbo.CounselorHighSchool", name: "CounselorsId", newName: "HighSchoolGraduationsId");
            RenameColumn(table: "dbo.CouncelorComment", name: "CounselorsId", newName: "CommentsId");
            RenameColumn(table: "dbo.CounselorCource", name: "CounselorsId", newName: "CounselorCourcesId");
            CreateIndex("dbo.Counselors", "CounselorCourcesId");
        }
    }
}
