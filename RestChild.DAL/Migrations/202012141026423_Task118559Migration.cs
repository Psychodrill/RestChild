namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task118559Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MGTVisitTarget", "IsForMPGU", c => c.Boolean(nullable: false, defaultValue: true));
            DropColumn("dbo.MGTVisitTarget", "SlotsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MGTVisitTarget", "SlotsCount", c => c.Int());
            DropColumn("dbo.MGTVisitTarget", "IsForMPGU");
        }
    }
}
