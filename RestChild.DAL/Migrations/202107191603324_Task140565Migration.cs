namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task140565Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicant", "IsCPMPK", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Child", "IsCPMPK", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Child", "IsCPMPK");
            DropColumn("dbo.Applicant", "IsCPMPK");
        }
    }
}
