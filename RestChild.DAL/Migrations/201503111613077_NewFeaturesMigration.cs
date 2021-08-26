namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFeaturesMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BenefitType", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.BenefitType", "TypeOfRestId", c => c.Long());
            AddColumn("dbo.Hotels", "AccessibleEnvironment", c => c.Boolean(nullable: false));
            CreateIndex("dbo.BenefitType", "TypeOfRestId");
            AddForeignKey("dbo.BenefitType", "TypeOfRestId", "dbo.TypeOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BenefitType", "TypeOfRestId", "dbo.TypeOfRest");
            DropIndex("dbo.BenefitType", new[] { "TypeOfRestId" });
            DropColumn("dbo.Hotels", "AccessibleEnvironment");
            DropColumn("dbo.BenefitType", "TypeOfRestId");
            DropColumn("dbo.BenefitType", "IsActive");
        }
    }
}
