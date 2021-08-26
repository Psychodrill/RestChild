namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotRequaredTourNameMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tour", "Name", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tour", "Name", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
