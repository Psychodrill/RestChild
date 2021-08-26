namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeclineReasonFlags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeclineReason", "FirstStage", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeclineReason", "SecondStage", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeclineReason", "IsManual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeclineReason", "IsManual");
            DropColumn("dbo.DeclineReason", "SecondStage");
            DropColumn("dbo.DeclineReason", "FirstStage");
        }
    }
}
