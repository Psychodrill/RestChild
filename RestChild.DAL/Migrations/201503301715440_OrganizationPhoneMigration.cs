namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationPhoneMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "Phone", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organization", "Phone");
        }
    }
}
