namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task96213Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganisatorCollaborator", "Filled", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganisatorCollaborator", "Filled");
        }
    }
}
