namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueMemberMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChildUniqe",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Snils = c.String(maxLength: 100),
                        LastInfoId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Child", t => t.LastInfoId)
                .Index(t => t.LastInfoId)
                .Index(t => t.Snils)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RelativeUniqe",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Snils = c.String(maxLength: 100),
                        LastInfoId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.LastInfoId)
                .Index(t => t.LastInfoId)
                .Index(t => t.Eid)
                .Index(t => t.Snils)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RelativeUniqeApplicant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestId = c.Long(),
                        RelativeUniqeId = c.Long(),
                        ApplicantId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.RelativeUniqe", t => t.RelativeUniqeId)
                .Index(t => t.RequestId)
                .Index(t => t.RelativeUniqeId)
                .Index(t => t.ApplicantId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.ChildUniqeRelativeUniqe",
                c => new
                    {
                        ChildUniqe_Id = c.Long(nullable: false),
                        RelativeUniqe_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildUniqe_Id, t.RelativeUniqe_Id })
                .ForeignKey("dbo.ChildUniqe", t => t.ChildUniqe_Id, cascadeDelete: true)
                .ForeignKey("dbo.RelativeUniqe", t => t.RelativeUniqe_Id, cascadeDelete: true)
                .Index(t => t.ChildUniqe_Id)
                .Index(t => t.RelativeUniqe_Id);
            
            AddColumn("dbo.Applicant", "RelativeUniqeId", c => c.Long());
            AddColumn("dbo.Child", "ChildUniqeId", c => c.Long());
            CreateIndex("dbo.Applicant", "RelativeUniqeId");
            CreateIndex("dbo.Child", "ChildUniqeId");
            AddForeignKey("dbo.Child", "ChildUniqeId", "dbo.ChildUniqe", "Id");
            AddForeignKey("dbo.Applicant", "RelativeUniqeId", "dbo.RelativeUniqe", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant", "RelativeUniqeId", "dbo.RelativeUniqe");
            DropForeignKey("dbo.Child", "ChildUniqeId", "dbo.ChildUniqe");
            DropForeignKey("dbo.ChildUniqeRelativeUniqe", "RelativeUniqe_Id", "dbo.RelativeUniqe");
            DropForeignKey("dbo.ChildUniqeRelativeUniqe", "ChildUniqe_Id", "dbo.ChildUniqe");
            DropForeignKey("dbo.RelativeUniqeApplicant", "RelativeUniqeId", "dbo.RelativeUniqe");
            DropForeignKey("dbo.RelativeUniqeApplicant", "RequestId", "dbo.Request");
            DropForeignKey("dbo.RelativeUniqeApplicant", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.RelativeUniqe", "LastInfoId", "dbo.Applicant");
            DropForeignKey("dbo.ChildUniqe", "LastInfoId", "dbo.Child");
            DropIndex("dbo.ChildUniqeRelativeUniqe", new[] { "RelativeUniqe_Id" });
            DropIndex("dbo.ChildUniqeRelativeUniqe", new[] { "ChildUniqe_Id" });
            DropIndex("dbo.RelativeUniqeApplicant", new[] { "EidSendStatus" });
            DropIndex("dbo.RelativeUniqeApplicant", new[] { "Eid" });
            DropIndex("dbo.RelativeUniqeApplicant", new[] { "ApplicantId" });
            DropIndex("dbo.RelativeUniqeApplicant", new[] { "RelativeUniqeId" });
            DropIndex("dbo.RelativeUniqeApplicant", new[] { "RequestId" });
            DropIndex("dbo.RelativeUniqe", new[] { "EidSendStatus" });
            DropIndex("dbo.RelativeUniqe", new[] { "Eid" });
            DropIndex("dbo.RelativeUniqe", new[] { "Snils" });
            DropIndex("dbo.RelativeUniqe", new[] { "LastInfoId" });
            DropIndex("dbo.ChildUniqe", new[] { "EidSendStatus" });
            DropIndex("dbo.ChildUniqe", new[] { "Eid" });
            DropIndex("dbo.ChildUniqe", new[] { "Snils" });
            DropIndex("dbo.ChildUniqe", new[] { "LastInfoId" });
            DropIndex("dbo.Child", new[] { "ChildUniqeId" });
            DropIndex("dbo.Applicant", new[] { "RelativeUniqeId" });
            DropColumn("dbo.Child", "ChildUniqeId");
            DropColumn("dbo.Applicant", "RelativeUniqeId");
            DropTable("dbo.ChildUniqeRelativeUniqe");
            DropTable("dbo.RelativeUniqeApplicant");
            DropTable("dbo.RelativeUniqe");
            DropTable("dbo.ChildUniqe");
        }
    }
}
