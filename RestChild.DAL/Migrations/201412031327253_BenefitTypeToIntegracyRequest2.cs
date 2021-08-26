namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenefitTypeToIntegracyRequest2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BenefitTypeInteragencyRequest", "BenefitType_Id", "dbo.BenefitType");
            DropForeignKey("dbo.BenefitTypeInteragencyRequest", "InteragencyRequest_Id", "dbo.InteragencyRequest");
            DropIndex("dbo.BenefitTypeInteragencyRequest", new[] { "BenefitType_Id" });
            DropIndex("dbo.BenefitTypeInteragencyRequest", new[] { "InteragencyRequest_Id" });
            CreateTable(
                "dbo.InteragencyRequestBenefitType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InteragencyRequestId = c.Long(),
                        BenefitTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BenefitType", t => t.BenefitTypeId)
                .ForeignKey("dbo.InteragencyRequest", t => t.InteragencyRequestId)
                .Index(t => t.InteragencyRequestId)
                .Index(t => t.BenefitTypeId);
            
            DropTable("dbo.BenefitTypeInteragencyRequest");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BenefitTypeInteragencyRequest",
                c => new
                    {
                        BenefitType_Id = c.Long(nullable: false),
                        InteragencyRequest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.BenefitType_Id, t.InteragencyRequest_Id });
            
            DropForeignKey("dbo.InteragencyRequestBenefitType", "InteragencyRequestId", "dbo.InteragencyRequest");
            DropForeignKey("dbo.InteragencyRequestBenefitType", "BenefitTypeId", "dbo.BenefitType");
            DropIndex("dbo.InteragencyRequestBenefitType", new[] { "BenefitTypeId" });
            DropIndex("dbo.InteragencyRequestBenefitType", new[] { "InteragencyRequestId" });
            DropTable("dbo.InteragencyRequestBenefitType");
            CreateIndex("dbo.BenefitTypeInteragencyRequest", "InteragencyRequest_Id");
            CreateIndex("dbo.BenefitTypeInteragencyRequest", "BenefitType_Id");
            AddForeignKey("dbo.BenefitTypeInteragencyRequest", "InteragencyRequest_Id", "dbo.InteragencyRequest", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BenefitTypeInteragencyRequest", "BenefitType_Id", "dbo.BenefitType", "Id", cascadeDelete: true);
        }
    }
}
