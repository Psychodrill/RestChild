namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountRightForOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccessRight", "ForOrganization", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccessRight", "ForOrganization");
        }
    }
}
