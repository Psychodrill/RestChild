namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrpphanBlockMigrationPupilGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PupilGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        PupilsCount = c.Int(),
                        CollaboratorsCount = c.Int(),
                        MGTCollaboratorsCount = c.Int(),
                        OrganizationId = c.Long(),
                        StateId = c.Long(),
                        HistoryLinkId = c.Long(),
                        FormOfRestId = c.Long(),
                        YearOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormOfRest", t => t.FormOfRestId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.OrganizationId)
                .Index(t => t.StateId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.FormOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.FormOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
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
                "dbo.PupilsHealthStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PupilsCount = c.Int(),
                        TypeOfRestrictionId = c.Long(),
                        TypeOfSubRestrictionId = c.Long(),
                        PupilGroupId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeOfRestriction", t => t.TypeOfRestrictionId)
                .ForeignKey("dbo.TypeOfSubRestriction", t => t.TypeOfSubRestrictionId)
                .ForeignKey("dbo.PupilGroup", t => t.PupilGroupId)
                .Index(t => t.TypeOfRestrictionId)
                .Index(t => t.TypeOfSubRestrictionId)
                .Index(t => t.PupilGroupId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RequestForPeriodOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TimeOfRestId = c.Long(),
                        PlaceOfRestId = c.Long(),
                        TourId = c.Long(),
                        PupilGroupId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRestId)
                .ForeignKey("dbo.Tour", t => t.TourId)
                .ForeignKey("dbo.PupilGroup", t => t.PupilGroupId)
                .Index(t => t.TimeOfRestId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.TourId)
                .Index(t => t.PupilGroupId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.PupilPupilGroup",
                c => new
                    {
                        Pupil_Id = c.Long(nullable: false),
                        PupilGroup_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pupil_Id, t.PupilGroup_Id })
                .ForeignKey("dbo.Pupil", t => t.Pupil_Id, cascadeDelete: true)
                .ForeignKey("dbo.PupilGroup", t => t.PupilGroup_Id, cascadeDelete: true)
                .Index(t => t.Pupil_Id)
                .Index(t => t.PupilGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PupilPupilGroup", "PupilGroup_Id", "dbo.PupilGroup");
            DropForeignKey("dbo.PupilPupilGroup", "Pupil_Id", "dbo.Pupil");
            DropForeignKey("dbo.PupilGroup", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.PupilGroup", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.RequestForPeriodOfRest", "PupilGroupId", "dbo.PupilGroup");
            DropForeignKey("dbo.RequestForPeriodOfRest", "TourId", "dbo.Tour");
            DropForeignKey("dbo.RequestForPeriodOfRest", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.RequestForPeriodOfRest", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.PupilsHealthStatus", "PupilGroupId", "dbo.PupilGroup");
            DropForeignKey("dbo.PupilsHealthStatus", "TypeOfSubRestrictionId", "dbo.TypeOfSubRestriction");
            DropForeignKey("dbo.PupilsHealthStatus", "TypeOfRestrictionId", "dbo.TypeOfRestriction");
            DropForeignKey("dbo.PupilGroup", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.PupilGroup", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.PupilGroup", "FormOfRestId", "dbo.FormOfRest");
            DropIndex("dbo.PupilPupilGroup", new[] { "PupilGroup_Id" });
            DropIndex("dbo.PupilPupilGroup", new[] { "Pupil_Id" });
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "Eid" });
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "PupilGroupId" });
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "TourId" });
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "PlaceOfRestId" });
            DropIndex("dbo.RequestForPeriodOfRest", new[] { "TimeOfRestId" });
            DropIndex("dbo.PupilsHealthStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilsHealthStatus", new[] { "Eid" });
            DropIndex("dbo.PupilsHealthStatus", new[] { "PupilGroupId" });
            DropIndex("dbo.PupilsHealthStatus", new[] { "TypeOfSubRestrictionId" });
            DropIndex("dbo.PupilsHealthStatus", new[] { "TypeOfRestrictionId" });
            DropIndex("dbo.FormOfRest", new[] { "EidSendStatus" });
            DropIndex("dbo.FormOfRest", new[] { "Eid" });
            DropIndex("dbo.PupilGroup", new[] { "EidSendStatus" });
            DropIndex("dbo.PupilGroup", new[] { "Eid" });
            DropIndex("dbo.PupilGroup", new[] { "YearOfRestId" });
            DropIndex("dbo.PupilGroup", new[] { "FormOfRestId" });
            DropIndex("dbo.PupilGroup", new[] { "HistoryLinkId" });
            DropIndex("dbo.PupilGroup", new[] { "StateId" });
            DropIndex("dbo.PupilGroup", new[] { "OrganizationId" });
            DropTable("dbo.PupilPupilGroup");
            DropTable("dbo.RequestForPeriodOfRest");
            DropTable("dbo.PupilsHealthStatus");
            DropTable("dbo.FormOfRest");
            DropTable("dbo.PupilGroup");
        }
    }
}
