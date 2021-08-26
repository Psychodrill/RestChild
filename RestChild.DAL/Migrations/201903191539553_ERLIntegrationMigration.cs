namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ERLIntegrationMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ERLBenefitStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsSent = c.Boolean(nullable: false),
                        AnswerRecived = c.Boolean(nullable: false),
                        BenefitUid = c.Guid(),
                        PersonId = c.Long(),
                        RequestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ERLPersonStatus", t => t.PersonId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.PersonId)
                .Index(t => t.RequestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.ERLPersonStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsSent = c.Boolean(nullable: false),
                        AnswerRecived = c.Boolean(nullable: false),
                        PersonUid = c.Guid(),
                        ApplicantId = c.Long(),
                        ChildId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .Index(t => t.ApplicantId)
                .Index(t => t.ChildId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ERLBenefitStatus", "RequestId", "dbo.Request");
            DropForeignKey("dbo.ERLBenefitStatus", "PersonId", "dbo.ERLPersonStatus");
            DropForeignKey("dbo.ERLPersonStatus", "ChildId", "dbo.Child");
            DropForeignKey("dbo.ERLPersonStatus", "ApplicantId", "dbo.Applicant");
            DropIndex("dbo.ERLPersonStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.ERLPersonStatus", new[] { "Eid" });
            DropIndex("dbo.ERLPersonStatus", new[] { "ChildId" });
            DropIndex("dbo.ERLPersonStatus", new[] { "ApplicantId" });
            DropIndex("dbo.ERLBenefitStatus", new[] { "EidSendStatus" });
            DropIndex("dbo.ERLBenefitStatus", new[] { "Eid" });
            DropIndex("dbo.ERLBenefitStatus", new[] { "RequestId" });
            DropIndex("dbo.ERLBenefitStatus", new[] { "PersonId" });
            DropTable("dbo.ERLPersonStatus");
            DropTable("dbo.ERLBenefitStatus");
        }
    }
}
