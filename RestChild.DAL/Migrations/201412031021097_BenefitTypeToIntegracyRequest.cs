namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenefitTypeToIntegracyRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InteragencyRequest", "BenefitTypeId", "dbo.BenefitType");
            DropIndex("dbo.InteragencyRequest", new[] { "BenefitTypeId" });
            CreateTable(
                "dbo.BenefitTypeInteragencyRequest",
                c => new
                    {
                        BenefitType_Id = c.Long(nullable: false),
                        InteragencyRequest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.BenefitType_Id, t.InteragencyRequest_Id })
                .ForeignKey("dbo.BenefitType", t => t.BenefitType_Id, cascadeDelete: true)
                .ForeignKey("dbo.InteragencyRequest", t => t.InteragencyRequest_Id, cascadeDelete: true)
                .Index(t => t.BenefitType_Id)
                .Index(t => t.InteragencyRequest_Id);
            
            DropColumn("dbo.InteragencyRequest", "BenefitTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InteragencyRequest", "BenefitTypeId", c => c.Long());
            DropForeignKey("dbo.BenefitTypeInteragencyRequest", "InteragencyRequest_Id", "dbo.InteragencyRequest");
            DropForeignKey("dbo.BenefitTypeInteragencyRequest", "BenefitType_Id", "dbo.BenefitType");
            DropIndex("dbo.BenefitTypeInteragencyRequest", new[] { "InteragencyRequest_Id" });
            DropIndex("dbo.BenefitTypeInteragencyRequest", new[] { "BenefitType_Id" });
            DropTable("dbo.BenefitTypeInteragencyRequest");
            CreateIndex("dbo.InteragencyRequest", "BenefitTypeId");
            AddForeignKey("dbo.InteragencyRequest", "BenefitTypeId", "dbo.BenefitType", "Id");
        }
    }
}
