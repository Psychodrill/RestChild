namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfRestBenefitRestrictionMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfRestBenefitRestriction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MinAge = c.Int(nullable: false),
                        MaxAge = c.Int(nullable: false),
                        BenefitTypeId = c.Long(),
                        TypeOfRestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BenefitType", t => t.BenefitTypeId)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .Index(t => t.BenefitTypeId)
                .Index(t => t.TypeOfRestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeOfRestBenefitRestriction", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TypeOfRestBenefitRestriction", "BenefitTypeId", "dbo.BenefitType");
            DropIndex("dbo.TypeOfRestBenefitRestriction", new[] { "TypeOfRestId" });
            DropIndex("dbo.TypeOfRestBenefitRestriction", new[] { "BenefitTypeId" });
            DropTable("dbo.TypeOfRestBenefitRestriction");
        }
    }
}
