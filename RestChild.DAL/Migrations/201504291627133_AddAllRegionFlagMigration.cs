namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAllRegionFlagMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InteragencyRequest", "ForAllRegion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InteragencyRequest", "ForAllRegion");
        }
    }
}
