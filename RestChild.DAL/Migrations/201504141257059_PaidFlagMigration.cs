namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaidFlagMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "Payed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "Payed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Child", "Payed");
            DropColumn("dbo.Applicant", "Payed");
        }
    }
}
