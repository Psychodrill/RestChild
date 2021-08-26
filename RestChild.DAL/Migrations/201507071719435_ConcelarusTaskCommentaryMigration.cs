namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcelarusTaskCommentaryMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CounselorTask", "AccountId", "dbo.Account");
            DropForeignKey("dbo.CounselorTask", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.CounselorTask", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.CounselorTask", "PartyId", "dbo.Party");
            DropIndex("dbo.CounselorTask", new[] { "AdministratorTourId" });
            DropIndex("dbo.CounselorTask", new[] { "CounselorsId" });
            DropIndex("dbo.CounselorTask", new[] { "BoutId" });
            DropIndex("dbo.CounselorTask", new[] { "PartyId" });
            DropIndex("dbo.CounselorTask", new[] { "AccountId" });
            CreateTable(
                "dbo.ResponsibilityForTask",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdministratorTourId = c.Long(),
                        CounselorsId = c.Long(),
                        BoutId = c.Long(),
                        PartyId = c.Long(),
                        AccountId = c.Long(),
                        CounselorTaskExecutorTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTourId)
                .ForeignKey("dbo.Bout", t => t.BoutId)
                .ForeignKey("dbo.CounselorTaskExecutorType", t => t.CounselorTaskExecutorTypeId)
                .ForeignKey("dbo.Party", t => t.PartyId)
                .Index(t => t.AdministratorTourId)
                .Index(t => t.CounselorsId)
                .Index(t => t.BoutId)
                .Index(t => t.PartyId)
                .Index(t => t.AccountId)
                .Index(t => t.CounselorTaskExecutorTypeId);
            
            CreateTable(
                "dbo.CounselorTaskExecutorType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CounselorTaskCommentary",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Commentary = c.String(),
                        Author = c.String(maxLength: 1000),
                        AdministratorTourId = c.Long(),
                        CounselorsId = c.Long(),
                        AccountId = c.Long(),
                        CounselorTaskId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .ForeignKey("dbo.AdministratorTour", t => t.AdministratorTourId)
                .ForeignKey("dbo.Counselors", t => t.CounselorsId)
                .ForeignKey("dbo.CounselorTask", t => t.CounselorTaskId)
                .Index(t => t.AdministratorTourId)
                .Index(t => t.CounselorsId)
                .Index(t => t.AccountId)
                .Index(t => t.CounselorTaskId);
            
            AddColumn("dbo.AdministratorTour", "ExternalUid", c => c.String(maxLength: 300));
            RenameColumn("dbo.Counselors", "RegistrationAddtress", "RegistrationAddress");
            AddColumn("dbo.Counselors", "ExternalUid", c => c.String(maxLength: 300));
            AddColumn("dbo.Counselors", "FactAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.CounselorTask", "NotNecessary", c => c.Boolean(nullable: false));
            AddColumn("dbo.CounselorTask", "AuthorId", c => c.Long());
            AddColumn("dbo.CounselorTask", "ExecutorId", c => c.Long());
            CreateIndex("dbo.CounselorTask", "AuthorId");
            CreateIndex("dbo.CounselorTask", "ExecutorId");
            AddForeignKey("dbo.CounselorTask", "AuthorId", "dbo.ResponsibilityForTask", "Id");
            AddForeignKey("dbo.CounselorTask", "ExecutorId", "dbo.ResponsibilityForTask", "Id");
            DropColumn("dbo.CounselorTask", "AdministratorTourId");

			DropForeignKey("dbo.CounselorTask", "CounselorsId", "dbo.Counselors");
			DropForeignKey("dbo.CounselorTask", "BoutId", "dbo.Bout");
			DropForeignKey("dbo.CounselorTask", "PartyId", "dbo.Party");
			DropForeignKey("dbo.CounselorTask", "AccountId", "dbo.Account");

            DropColumn("dbo.CounselorTask", "CounselorsId");
            DropColumn("dbo.CounselorTask", "BoutId");
            DropColumn("dbo.CounselorTask", "PartyId");
            DropColumn("dbo.CounselorTask", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CounselorTask", "AccountId", c => c.Long());
            AddColumn("dbo.CounselorTask", "PartyId", c => c.Long());
            AddColumn("dbo.CounselorTask", "BoutId", c => c.Long());
            AddColumn("dbo.CounselorTask", "CounselorsId", c => c.Long());
            AddColumn("dbo.CounselorTask", "AdministratorTourId", c => c.Long());
            AddColumn("dbo.Counselors", "RegistrationAddtress", c => c.String());
            DropForeignKey("dbo.CounselorTaskCommentary", "CounselorTaskId", "dbo.CounselorTask");
            DropForeignKey("dbo.CounselorTaskCommentary", "CounselorsId", "dbo.Counselors");
            DropForeignKey("dbo.CounselorTaskCommentary", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.CounselorTaskCommentary", "AccountId", "dbo.Account");
            DropForeignKey("dbo.CounselorTask", "ExecutorId", "dbo.ResponsibilityForTask");
            DropForeignKey("dbo.CounselorTask", "AuthorId", "dbo.ResponsibilityForTask");
            DropForeignKey("dbo.ResponsibilityForTask", "PartyId", "dbo.Party");
            DropForeignKey("dbo.ResponsibilityForTask", "CounselorTaskExecutorTypeId", "dbo.CounselorTaskExecutorType");
            DropForeignKey("dbo.ResponsibilityForTask", "BoutId", "dbo.Bout");
            DropForeignKey("dbo.ResponsibilityForTask", "AdministratorTourId", "dbo.AdministratorTour");
            DropForeignKey("dbo.ResponsibilityForTask", "AccountId", "dbo.Account");
            DropIndex("dbo.CounselorTaskCommentary", new[] { "CounselorTaskId" });
            DropIndex("dbo.CounselorTaskCommentary", new[] { "AccountId" });
            DropIndex("dbo.CounselorTaskCommentary", new[] { "CounselorsId" });
            DropIndex("dbo.CounselorTaskCommentary", new[] { "AdministratorTourId" });
            DropIndex("dbo.CounselorTask", new[] { "ExecutorId" });
            DropIndex("dbo.CounselorTask", new[] { "AuthorId" });
            DropIndex("dbo.ResponsibilityForTask", new[] { "CounselorTaskExecutorTypeId" });
            DropIndex("dbo.ResponsibilityForTask", new[] { "AccountId" });
            DropIndex("dbo.ResponsibilityForTask", new[] { "PartyId" });
            DropIndex("dbo.ResponsibilityForTask", new[] { "BoutId" });
            DropIndex("dbo.ResponsibilityForTask", new[] { "CounselorsId" });
            DropIndex("dbo.ResponsibilityForTask", new[] { "AdministratorTourId" });
            DropColumn("dbo.CounselorTask", "ExecutorId");
            DropColumn("dbo.CounselorTask", "AuthorId");
            DropColumn("dbo.CounselorTask", "NotNecessary");
            DropColumn("dbo.Counselors", "FactAddress");
            DropColumn("dbo.Counselors", "ExternalUid");
            DropColumn("dbo.Counselors", "RegistrationAddress");
            DropColumn("dbo.AdministratorTour", "ExternalUid");
            DropTable("dbo.CounselorTaskCommentary");
            DropTable("dbo.CounselorTaskExecutorType");
            DropTable("dbo.ResponsibilityForTask");
            CreateIndex("dbo.CounselorTask", "AccountId");
            CreateIndex("dbo.CounselorTask", "PartyId");
            CreateIndex("dbo.CounselorTask", "BoutId");
            CreateIndex("dbo.CounselorTask", "CounselorsId");
            CreateIndex("dbo.CounselorTask", "AdministratorTourId");
            AddForeignKey("dbo.CounselorTask", "PartyId", "dbo.Party", "Id");
            AddForeignKey("dbo.CounselorTask", "BoutId", "dbo.Bout", "Id");
            AddForeignKey("dbo.CounselorTask", "AdministratorTourId", "dbo.AdministratorTour", "Id");
            AddForeignKey("dbo.CounselorTask", "AccountId", "dbo.Account", "Id");
        }
    }
}
