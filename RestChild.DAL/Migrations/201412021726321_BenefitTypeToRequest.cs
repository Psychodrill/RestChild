namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenefitTypeToRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InteragencyRequest", "BenefitTypeId", c => c.Long());
            CreateIndex("dbo.InteragencyRequest", "BenefitTypeId");
            AddForeignKey("dbo.InteragencyRequest", "BenefitTypeId", "dbo.BenefitType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InteragencyRequest", "BenefitTypeId", "dbo.BenefitType");
            DropIndex("dbo.InteragencyRequest", new[] { "BenefitTypeId" });
            DropColumn("dbo.InteragencyRequest", "BenefitTypeId");
        }
    }
}
