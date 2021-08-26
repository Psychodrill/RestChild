namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NeedTypeOfRestriction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BenefitType", "NeedTypeOfRestriction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BenefitType", "NeedTypeOfRestriction");
        }
    }
}
