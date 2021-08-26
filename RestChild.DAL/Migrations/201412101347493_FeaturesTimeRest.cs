namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeaturesTimeRest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeOfRest", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.TimeOfRest", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeOfRest", "IsActive");
            DropColumn("dbo.TimeOfRest", "Year");
        }
    }
}
