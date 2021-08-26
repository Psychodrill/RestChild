namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegrationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "EkisId", c => c.Long());
            AddColumn("dbo.Tour", "EkisNeedSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Child", "EkisId", c => c.Long());
            AddColumn("dbo.Child", "EkisNeedSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Hotels", "EkisId", c => c.Long());
            AddColumn("dbo.Hotels", "EkisNeedSend", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "EkisNeedSend");
            DropColumn("dbo.Hotels", "EkisId");
            DropColumn("dbo.Child", "EkisNeedSend");
            DropColumn("dbo.Child", "EkisId");
            DropColumn("dbo.Tour", "EkisNeedSend");
            DropColumn("dbo.Tour", "EkisId");
        }
    }
}
