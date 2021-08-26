namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VocsUpdateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusByChild", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusByChild", "IsActive");
        }
    }
}
