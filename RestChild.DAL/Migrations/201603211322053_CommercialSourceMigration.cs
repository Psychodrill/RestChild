namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommercialSourceMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Source", "Commercial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Source", "Commercial");
        }
    }
}
