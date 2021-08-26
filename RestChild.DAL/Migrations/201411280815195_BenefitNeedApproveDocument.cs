namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenefitNeedApproveDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BenefitType", "NeedApproveDocument", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BenefitType", "NeedApproveDocument");
        }
    }
}
