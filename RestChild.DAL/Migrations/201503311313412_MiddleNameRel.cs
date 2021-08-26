namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MiddleNameRel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agent", "HaveMiddleName", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agent", "HaveMiddleName");
        }
    }
}
