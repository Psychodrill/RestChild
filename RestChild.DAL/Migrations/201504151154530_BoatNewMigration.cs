namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoatNewMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bout", "AdministratorId", "dbo.Counselors");
            DropForeignKey("dbo.Bout", "SeniorCounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.Bout", "TimeOfRestId", "dbo.TimeOfRest");
            DropIndex("dbo.Bout", new[] { "SeniorCounselorsId" });
            DropIndex("dbo.Bout", new[] { "AdministratorId" });
            DropIndex("dbo.Bout", new[] { "TimeOfRestId" });
            CreateTable(
                "dbo.GroupedTimeOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdministratorTour",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        HaveMiddleName = c.Boolean(nullable: false),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        Male = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateUpdate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdministratorTourBout",
                c => new
                    {
                        AdministratorTour_Id = c.Long(nullable: false),
                        Bout_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AdministratorTour_Id, t.Bout_Id })
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTour_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bout", t => t.Bout_Id, cascadeDelete: true)
                .Index(t => t.AdministratorTour_Id)
                .Index(t => t.Bout_Id);
            
            CreateTable(
                "dbo.BoutCounselors",
                c => new
                    {
                        Bout_Id = c.Long(nullable: false),
                        Counselors_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bout_Id, t.Counselors_Id })
                .ForeignKey("dbo.Bout", t => t.Bout_Id, cascadeDelete: true)
                .ForeignKey("dbo.Counselors", t => t.Counselors_Id, cascadeDelete: true)
                .Index(t => t.Bout_Id)
                .Index(t => t.Counselors_Id);
            
            CreateTable(
                "dbo.BoutCounselors1",
                c => new
                    {
                        Bout_Id = c.Long(nullable: false),
                        Counselors_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bout_Id, t.Counselors_Id })
                .ForeignKey("dbo.Bout", t => t.Bout_Id, cascadeDelete: true)
                .ForeignKey("dbo.Counselors", t => t.Counselors_Id, cascadeDelete: true)
                .Index(t => t.Bout_Id)
                .Index(t => t.Counselors_Id);
            
            AddColumn("dbo.TimeOfRest", "GroupedTimeOfRestId", c => c.Long());
            AddColumn("dbo.Bout", "GroupedTimeOfRestId", c => c.Long());
            AddColumn("dbo.Bout", "SubjectOfRestId", c => c.Long());
            CreateIndex("dbo.TimeOfRest", "GroupedTimeOfRestId");
            CreateIndex("dbo.Bout", "GroupedTimeOfRestId");
            CreateIndex("dbo.Bout", "SubjectOfRestId");
            AddForeignKey("dbo.TimeOfRest", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest", "Id");
            AddForeignKey("dbo.Bout", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest", "Id");
            AddForeignKey("dbo.Bout", "SubjectOfRestId", "dbo.SubjectOfRest", "Id");
            DropColumn("dbo.Bout", "SeniorCounselorsId");
            DropColumn("dbo.Bout", "AdministratorId");
            DropColumn("dbo.Bout", "TimeOfRestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bout", "TimeOfRestId", c => c.Long());
            AddColumn("dbo.Bout", "AdministratorId", c => c.Long());
            AddColumn("dbo.Bout", "SeniorCounselorsId", c => c.Long());
            DropForeignKey("dbo.BoutCounselors1", "Counselors_Id", "dbo.Counselors");
            DropForeignKey("dbo.BoutCounselors1", "Bout_Id", "dbo.Bout");
            DropForeignKey("dbo.Bout", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropForeignKey("dbo.BoutCounselors", "Counselors_Id", "dbo.Counselors");
            DropForeignKey("dbo.BoutCounselors", "Bout_Id", "dbo.Bout");
            DropForeignKey("dbo.Bout", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest");
            DropForeignKey("dbo.AdministratorTourBout", "Bout_Id", "dbo.Bout");
            DropForeignKey("dbo.AdministratorTourBout", "AdministratorTour_Id", "dbo.AdministratorTour");
            DropForeignKey("dbo.TimeOfRest", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest");
            DropIndex("dbo.BoutCounselors1", new[] { "Counselors_Id" });
            DropIndex("dbo.BoutCounselors1", new[] { "Bout_Id" });
            DropIndex("dbo.BoutCounselors", new[] { "Counselors_Id" });
            DropIndex("dbo.BoutCounselors", new[] { "Bout_Id" });
            DropIndex("dbo.AdministratorTourBout", new[] { "Bout_Id" });
            DropIndex("dbo.AdministratorTourBout", new[] { "AdministratorTour_Id" });
            DropIndex("dbo.Bout", new[] { "SubjectOfRestId" });
            DropIndex("dbo.Bout", new[] { "GroupedTimeOfRestId" });
            DropIndex("dbo.TimeOfRest", new[] { "GroupedTimeOfRestId" });
            DropColumn("dbo.Bout", "SubjectOfRestId");
            DropColumn("dbo.Bout", "GroupedTimeOfRestId");
            DropColumn("dbo.TimeOfRest", "GroupedTimeOfRestId");
            DropTable("dbo.BoutCounselors1");
            DropTable("dbo.BoutCounselors");
            DropTable("dbo.AdministratorTourBout");
            DropTable("dbo.AdministratorTour");
            DropTable("dbo.GroupedTimeOfRest");
            CreateIndex("dbo.Bout", "TimeOfRestId");
            CreateIndex("dbo.Bout", "AdministratorId");
            CreateIndex("dbo.Bout", "SeniorCounselorsId");
            AddForeignKey("dbo.Bout", "TimeOfRestId", "dbo.TimeOfRest", "Id");
            AddForeignKey("dbo.Bout", "SeniorCounselorsId", "dbo.Counselors", "Id");
            AddForeignKey("dbo.Bout", "AdministratorId", "dbo.Counselors", "Id");
        }
    }
}
