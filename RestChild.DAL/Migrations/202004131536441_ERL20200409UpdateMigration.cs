namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ERL20200409UpdateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ERLBenefitStatus", "ERLCommited", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.ERLPersonStatus", "ERLCommited", c => c.Boolean(nullable: false, defaultValue: false));

            Sql("Update [dbo].[ERLBenefitStatus] Set [ERLCommited] = 1");
            Sql("Update [dbo].[ERLPersonStatus] Set [ERLCommited] = 1");
        }
        
        public override void Down()
        {
            DropColumn("dbo.ERLPersonStatus", "ERLCommited");
            DropColumn("dbo.ERLBenefitStatus", "ERLCommited");
        }
    }
}
